using System;
using Xunit;

using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace WinEventLog.Test
{
    public class EventLogTest
    {
        const string LOG_SOURCE_NAME = "TEST-SOURCE";

        const string LOG_NAME = "testLog";

        [Fact]
        public void EventLogWrite()
        {
            // Windows Visita+ Must be Administrator!
            if (!EventLog.SourceExists(LOG_SOURCE_NAME))
            {
                EventLog.CreateEventSource(LOG_SOURCE_NAME, LOG_NAME);
                return;
            }
            EventLog myLog = new EventLog();
            myLog.Source = LOG_SOURCE_NAME;
            myLog.WriteEntry("Writing to event log.");
        }

        [Fact]
        public void EventLogQuery()
        {            // Query EevntLog
            EventLogReader logReader = 
                new EventLogReader(new EventLogQuery("testLog",
                PathType.LogName, "*"));
            EventRecord eventRecord = logReader.ReadEvent();
            Assert.Equal(LOG_SOURCE_NAME, eventRecord.ProviderName);
        }
    }
}
