﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2015_Pre_build_week_project.Autonomous.Commands;

namespace _2015_Pre_build_week_project.Autonomous
{
    public static class AutonRoutines
    {
        public static Queue<AutonCommand> DoNothing;
        public static Queue<AutonCommand> Straight;
        public static Queue<AutonCommand> Angled;
        public static Queue<AutonCommand> UTurn;

        static AutonRoutines()
        {
            //Do Nothing
            DoNothing = new Queue<AutonCommand>();

            //Straight
            Straight = new Queue<AutonCommand>();
            Straight.Enqueue(new AutonReadyCommand());
            Straight.Enqueue(new IntakeSetCommand(true));
            Straight.Enqueue(new DriveForwardCommand(110, 60));
            Straight.Enqueue(new IntakeSetCommand(false));
            Straight.Enqueue(new OutputSetCommand(true));

            //Angled
            Angled = new Queue<AutonCommand>();
            Angled.Enqueue(new AutonReadyCommand());
            Angled.Enqueue(new IntakeSetCommand(true));
            Angled.Enqueue(new DriveForwardCommand(65, 10));
            Angled.Enqueue(new DriveTurnCommand(45, 10));
            Angled.Enqueue(new DriveForwardCommand(60, 10));
            Angled.Enqueue(new IntakeSetCommand(false));
            Angled.Enqueue(new OutputSetCommand(true));

            //U-Turn
            UTurn = new Queue<AutonCommand>();
            UTurn.Enqueue(new IntakeSetCommand(true));
            UTurn.Enqueue(new DriveForwardCommand(24, 2));
            UTurn.Enqueue(new DriveTurnCommand(90, 2));
            UTurn.Enqueue(new DriveForwardCommand(60, 2));
            UTurn.Enqueue(new DriveTurnCommand(90, 2));
            UTurn.Enqueue(new DriveForwardCommand(120, 3));
            UTurn.Enqueue(new IntakeSetCommand(false));
            UTurn.Enqueue(new OutputSetCommand(true));
        }
    }
}
