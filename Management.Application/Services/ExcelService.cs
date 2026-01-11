using System;
using System.Collections.Generic;
using Management.Application.Interfaces;
using Management.Domain.Models;

namespace Management.Application.Services
{
    public class ExcelService
    {
        private readonly IStudentExcelBroker _studentExcelBroker;

        public ExcelService(IStudentExcelBroker studentExcelBroker)
        {
            _studentExcelBroker = studentExcelBroker;
        }

        public byte[] ExportUsers()
        {
            var students = new List<Student>
            {
                new(id: new Random().Next(1, 1000).ToString(), "Xushnudbek", "Abdusamatov"),
                new(id: new Random().Next(1, 1000).ToString(), "Jamshidbek", "Aliyev"),
                new(id: new Random().Next(1, 1000).ToString(), "Bunyodbek", "Aburazzoqov"),
                new(id: new Random().Next(1, 1000).ToString(), "Samandar", "Hamraqulov"),
                new(id: new Random().Next(1, 1000).ToString(), "Ozodbek", "Nazarbekov"),
            };

            return _studentExcelBroker.Export(students);
        }
    }
}
