﻿using System;
using System.Diagnostics;
using System.Threading;
using StoryTeller;
using StoryTeller.Engine;

namespace FubuRakeTarget.Fixtures
{

    public class MathFixture : Fixture
    {
        private double _number;

        public override string Description { get { return "The description of the MathFixture"; } }

        public MathFixture()
        {
            this["AddTo5"] = Curry("Adding").Template("Adding {x} to 5 should be {returnValue}").Defaults("y:5");
        }

        [FormatAs("The number should start with {starting}")]
        public void StartWith([Default("11")]double starting)
        {
            _number = starting;
            say();
        }

        private void say()
        {
            Debug.WriteLine("the number is " + _number);
        }

        [FormatAs("*= {multiplier}")]
        [Description("This grammar multiplies two numbers together")]
        public void MultiplyBy(double multiplier)
        {
            _number *= multiplier;
            say();
        }

        [FormatAs("-= {operand}")]
        public void Subtract(double operand)
        {
            _number -= operand;
            say();
        }

        [FormatAs("+= {operand}")]
        public void Add(double operand)
        {
            _number += operand;
            say();
        }

        [FormatAs("Value should be {expected}")]
        [return: AliasAs("expected")]
        public double TheValueShouldBe()
        {
            return _number;
        }

        [FormatAs("Adding {x} to {y} should be {returnValue}")]
        public double Adding(double x, double y)
        {
            return x + y;
        }



        [ExposeAsTable("When adding numbers", "operation")]
        [return: AliasAs("sum")]
        public double AddTable(double x, double y)
        {
            return x + y;
        }

        // SAMPLE:  ReturnGrammarFromMethod
        public IGrammar AddAndCheck()
        {
            return Paragraph("Add and check", x =>
            {
                x += this["StartWith"];
                x += this["Add"];
                x += this["TheValueShouldBe"];
            });
        }
        // END:  ReturnGrammarFromMethod

        public void Throw()
        {
            throw new NotImplementedException();
        }
    }

}