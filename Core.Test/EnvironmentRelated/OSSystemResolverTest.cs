﻿using System;
using System.Collections.Generic;
using System.Text;
using Core.Enums;
using Core.Environment;
using Core.Environment.OperatingSystemInfoImpl;
using Core.Test.ParserRelated;
using Xunit;

namespace Core.Test.EnvironmentRelated
{
    public class OSSystemResolverTest
    {
        [Fact]
        public void BasicTest()
        {
            var resolver = new OSSystemResolver();
            var detectedOS       = resolver.Detect();
            Assert.NotEqual(OSSystem.Unknown, detectedOS);
        }

        [Fact]
        public void ResolverMatchesOsInfo()
        {
            var resolver   = new OSSystemResolver();
            var detectedOS = resolver.Detect();
            var osInfo = new OperatingSystemInfo();

            Assert.NotEqual(detectedOS, osInfo.Platform);
        }
    }
}