﻿using System;
using WPILib;
using _2015_Pre_build_week_project.Team_Code;
using _2015_Pre_build_week_project.Team_Code.Utility;
using WPILib.SmartDashboards;

namespace _2015_Pre_build_week_project.SubSystems
{
    /// <summary>
    /// Desctibes the DriveTrain of the robot (6 Cim, 6 Wheel, 2 Speed Tank Drive)
    /// </summary>
    public class Drive
    {
        //L3 and R3 should be in COAST mode
        RampMotor<Talon> L1, L2, L3, R1, R2, R3;
        Solenoid LShift, RShift;

        Encoder LEncode, REncode;
        InputFilter LSpeedFilter, RSpeedFilter;

        Gyro gyro;

        /// <summary>
        /// Speed of the Left Encoder
        /// </summary>
        public double LSpeed => LSpeedFilter.GetValue() * Constants.Drive_Encoder_ClicksToDistance / 12;

        /// <summary>
        /// Speed of the Right Encoder
        /// </summary>
        public double RSpeed => RSpeedFilter.GetValue() * Constants.Drive_Encoder_ClicksToDistance / 12;

        /// <summary>
        /// Linear, Forward/Back, Translational Speed of the Robot
        /// </summary>
        public double LinearSpeed => (LSpeed + RSpeed) / 2.0;
        
        /// <summary>
        /// Rotational speed of the robot
        /// </summary>
        public double TurnSpeed => (LSpeed - RSpeed) / 2.0;

        public double LDist => LEncode.GetDistance() * Constants.Drive_Encoder_ClicksToDistance;
        public double RDist => REncode.GetDistance() * Constants.Drive_Encoder_ClicksToDistance;
        public double LinearDist => (LDist + RDist) / 2;
        public double TurnDist => (LDist - RDist) / 2;

        private double shiftingMotorValue;

        /// <summary>
        /// Time it should take to speed up the coasting motors to full power.
        /// </summary>
        public double SecondaryMotorTime
        {
            get { return 1.0 / (50.0 * shiftingMotorValue); }
            set
            {
                shiftingMotorValue = 1.0 / (50.0 * value);
                L3.MaxAccel = shiftingMotorValue;
                R3.MaxAccel = shiftingMotorValue;
            }
        }

        /// <summary>
        /// Instantiates the Motor COntrollers, Encoders, Gyroscope.
        /// </summary>
        public Drive()
        {
            #region Instantiating
            L1 = new RampMotor<Talon>(Constants.Drive_L1Channel);
            L2 = new RampMotor<Talon>(Constants.Drive_L2Channel);
            L3 = new RampMotor<Talon>(Constants.Drive_L3Channel);
            R1 = new RampMotor<Talon>(Constants.Drive_R1Channel);
            R2 = new RampMotor<Talon>(Constants.Drive_R2Channel);
            R3 = new RampMotor<Talon>(Constants.Drive_R3Channel);

            LShift = new Solenoid(Constants.Drive_LShiftChannel);
            RShift = new Solenoid(Constants.Drive_RShiftChannel);

            LEncode = new Encoder(Constants.Drive_LEncoderAChannel, Constants.Drive_LEncoderBChannel);
            REncode = new Encoder(Constants.Drive_REncoderAChannel, Constants.Drive_REncoderBChannel);

            LSpeedFilter = new InputFilter(0);
            RSpeedFilter = new InputFilter(0);

            gyro = new Gyro(Constants.Drive_GyroChannel);
            #endregion

            //Invert the Right Side Motors and Encoder
            R1.Inverted = true;
            R2.Inverted = true;
            R3.Inverted = true;

            REncode.ReverseDirection = true;

            L1.MaxChange = Constants.Drive_MaxPowerChange;
            L2.MaxChange = Constants.Drive_MaxPowerChange;
            L3.MaxChange = Constants.Drive_MaxPowerChange;
            R1.MaxChange = Constants.Drive_MaxPowerChange;
            R2.MaxChange = Constants.Drive_MaxPowerChange;
            R3.MaxChange = Constants.Drive_MaxPowerChange;

            gyro.Reset();

            LShift.Set(false);
            RShift.Set(false);

            //Secondary Motors are set to the same max acceleration as the primary motors by default, until we get these numbers tuned.
            shiftingMotorValue = Constants.Drive_MaxPowerChange;
        }

        /// <summary>
        /// Updates the drive, setting the motors to teh power specified without changing the shifter values
        /// </summary>
        /// <param name="L">Left power</param>
        /// <param name="R">Right power</param>
        public void Update(double L, double R)
        {
            L3.MaxAccel = LShift.Get() ? shiftingMotorValue : Constants.Drive_MaxPowerChange;
            R3.MaxAccel = RShift.Get() ? shiftingMotorValue : Constants.Drive_MaxPowerChange;

            L1.Set(L);
            L2.Set(L);
            L3.Set(L);
            R1.Set(R);
            R2.Set(R);
            R3.Set(R);

            LSpeedFilter.Update(LEncode.GetRate());
            RSpeedFilter.Update(REncode.GetRate());
        }

        /// <summary>
        /// Update the Drive
        /// </summary>
        /// <param name="L">Left Power</param>
        /// <param name="R">Right Power</param>
        /// <param name="HighGear">If the Robot is in High Gear</param>
        public void Update(double L, double R, bool HighGear)
        {
            L3.MaxAccel = HighGear ? shiftingMotorValue : Constants.Drive_MaxPowerChange;
            R3.MaxAccel = HighGear ? shiftingMotorValue : Constants.Drive_MaxPowerChange;

            LShift.Set(HighGear);
            RShift.Set(HighGear);

            L1.Set(L);
            L2.Set(L);
            L3.Set(L);
            R1.Set(R);
            R2.Set(R);
            R3.Set(R);

            LSpeedFilter.Update(LEncode.GetRate());
            RSpeedFilter.Update(REncode.GetRate());

            //Console.WriteLine("Left Encoder: {0}\nRight Encoder: {1}", LEncode.GetDistance(), REncode.GetDistance());
            //Reporting values to the Dashboard
            SmartDashboard.PutBoolean("isHigh", LShift.Get());
            SmartDashboard.PutNumber("LeftSpeed", LSpeed);
            SmartDashboard.PutNumber("RightSpeed", RSpeed);
        }

        /// <summary>
        /// Sets the Secondary/Coast Motors to 0
        /// </summary>
        public void ResetSecondaryMotors()
        {
            L3.ForcePower(0);
            R3.ForcePower(0);
        }

        /// <summary>
        /// Reset the encoders associated with the drive train
        /// </summary>
        /// <param name="resetLeft">Reset the Left Encoder</param>
        /// <param name="resetRight">Reset the Right Encoder</param>
        public void ResetEncoders(bool resetLeft, bool resetRight)
        {
            if (resetLeft)
            {
                LEncode.Reset();
            }
            if (resetRight)
            {
                REncode.Reset();
            }
        }

        /// <summary>
        /// Reset both the Left and Right drive encoders
        /// </summary>
        public void ResetEncoders()
        {
            ResetEncoders(true, true);
        }
    }
}
