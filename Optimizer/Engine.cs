﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConferenceScheduler.Entities;

namespace ConferenceScheduler.Optimizer
{
    /// <summary>
    /// Holds methods used to perform optimizations to determine conference schedules.
    /// </summary>
    public class Engine
    {
        /// <summary>
        /// Returns an optimized conference schedule based on the inputs.
        /// </summary>
        /// <param name="sessions">A list of sessions and their associated attributes.</param>
        /// <param name="rooms">A list of rooms that sessions can be held in along with their associated attributes.</param>
        /// <param name="timeslots">A list of time slots during which sessions can be delivered.</param>
        /// <param name="settings">A dictionary of configuration settings for the process.</param>
        /// <returns>A collection of assignments representing the room and Timeslot in which each session will be delivered.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "matrix"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "presenterIds"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "speakerIds"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "rooms"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "sessions"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "timeslots"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "settings"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        public IEnumerable<Assignment> Process(IEnumerable<Session> sessions, IEnumerable<Room> rooms, IEnumerable<Timeslot> timeslots, IDictionary<string, string> settings)
        {
            var result = new List<Assignment>();
            if (sessions != null && rooms != null && timeslots != null)
            {
                // Make sure there are enough slots/rooms for all of the sessions
                // TODO: Subtract out any times that specific rooms are not available
                if (sessions.Count() > (rooms.Count() * timeslots.Count()))
                    throw new Exceptions.NoFeasibleSolutionsException();

                // Create the presenter availability matrix
                var presenters = sessions.SelectMany(s => s.Presenters).Distinct();
                var timeslotIds = timeslots.Select(ts => ts.Id);
                var matrix = new PresenterAvailablityCollection(presenters, timeslotIds);
                if (!matrix.IsFeasible)
                    throw new Exceptions.NoFeasibleSolutionsException();


                //TODO: Add value 
            }
            return result;
        }
    }
}