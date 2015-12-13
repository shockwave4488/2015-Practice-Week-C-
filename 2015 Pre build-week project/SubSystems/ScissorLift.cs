using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPILib;
using _2015_Pre_build_week_project.Autonomous;
using _2015_Pre_build_week_project.Team_Code.Utility;
using _2015_Pre_build_week_project.Autonomous.Commands;

namespace _2015_Pre_build_week_project.SubSystems
{
    //DELETE THIS FILE BEFORE THE REPOSITORY IS MADE PUBLIC
    //Encapsulate everything related to the scissor lift within this class so it's easier to clean up when we delete this.

    /// <summary>
    /// Controls the drive-actuated scissor lift
    /// </summary>
    public class ScissorLift
    {
        public bool PTOEnabled { get; set; }
        private AnalogPotentiometer m_pot;
        private PID m_pid;
        private Drive m_drive;

        public double Setpoint
        {
            get { return m_pid.setpoint; }
            set { m_pid.setpoint = value; }
        }

        public ScissorLift(Drive drive)
        {
            m_pot = new AnalogPotentiometer(5);
            m_pid = new PID(0.67, 0.225, 0.5659);
            PTOEnabled = false;
            m_drive = drive;
        }

        public void Update()
        {
            if (!PTOEnabled)
                return;

            m_drive.Update(m_pid.Get(m_pot.Get()), m_pid.Get(m_pot.Get()));
            m_pid.Update(m_pot.Get());
        }

        public bool AtSetPoint(double tolerance)
        {
            return Math.Abs(Setpoint - m_pot.Get()) < tolerance;
        }
    }

    public class ScissorLiftCommand : Autonomous.AutonCommand
    {
        public static ScissorLift ScissorLift;
        private double m_setpoint;

        static ScissorLiftCommand()
        {
            ScissorLift = new ScissorLift(BuildWeek2015.drive);
        }

        public ScissorLiftCommand(double setpoint)
        {
            m_setpoint = setpoint;
        }

        public bool Execute()
        {
            ScissorLift.PTOEnabled = true;
            ScissorLift.Setpoint = m_setpoint;
            ScissorLift.Update();
            return ScissorLift.AtSetPoint(m_setpoint);
        }

        public double TimeOut { get; set; }
    }

    public class ScissorLiftOnOffCommand : AutonCommand
    {
        private bool m_on;

        public ScissorLiftOnOffCommand(bool on)
        {
            m_on = on;
        }

        public bool Execute()
        {
            ScissorLiftCommand.ScissorLift.PTOEnabled = m_on;
            return true;
        }

        public double TimeOut { get; set; }
    }

    public static class ScissorLiftRoutine
    {
        public static Queue<AutonCommand> Lift;

        static ScissorLiftRoutine()
        {
            Lift = new Queue<AutonCommand>();
            Lift.Enqueue(new AutonReadyCommand());
            Lift.Enqueue(new DriveForwardCommand(10, 5));
            Lift.Enqueue(new IntakeSetCommand(true));
            Lift.Enqueue(new FollowSplineCommand("Spline3.txt"));
            Lift.Enqueue(new ScissorLiftCommand(2.54));
            Lift.Enqueue(new ScissorLiftOnOffCommand(false));
        }
    }
}
