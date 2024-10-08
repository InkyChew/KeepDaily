﻿using DomainLayer.Models;

namespace ServiceLayer.IServices
{
    public interface IDayService
    {
        public List<Day> GetDays(int planId, string start, string end);
        public Day UpdateDay(Day day);
        public Day DeleteDay(int id);
    }
}
