using System;
using System.Collections.Generic;
using System.IO;
using Management.Application.Interfaces;
using Management.Domain.Models;
using OfficeOpenXml;

namespace Management.Infrastructure.Brokers
{
    public class StudentExcelBroker : IStudentExcelBroker
    {
        // [Obsolete("Obsolete")]
        public StudentExcelBroker()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public void Export(List<Student> students)
        {
            using (var package = new ExcelPackage())
            {
                var sheet = package.Workbook.Worksheets.Add("Students");

                // Labels
                sheet.Cells["A1"].LoadFromText("ID");
                sheet.Cells["B1"].LoadFromText("First Name");
                sheet.Cells["C1"].LoadFromText("Last Name");

                // Load all students
                sheet.Cells["A2"].LoadFromCollection<Student>(students);

                // Save all changes
                package.Save();

                // Move to Root Directory
                package.File.MoveTo("Students.xlsx");
            }
        }

        public List<Student> Import(string filePath)
        {
            var fileInfo = new FileInfo(filePath);

            if (!fileInfo.Exists)
                throw new FileNotFoundException($"File {filePath} not found");

            List<Student> students = [];

            using (var package = new ExcelPackage(fileInfo))
            {
                var sheet = package.Workbook.Worksheets["Students"];

                var keepRepeating = true;
                var row = 2;

                while (keepRepeating)
                {
                    var id = sheet.Cells[row, 1].Text;
                    var firstName = sheet.Cells[row, 2].Text;
                    var lastName = sheet.Cells[row, 3].Text;

                    if (
                        string.IsNullOrWhiteSpace(id)
                        && string.IsNullOrWhiteSpace(firstName)
                        && string.IsNullOrWhiteSpace(lastName)
                    )
                    {
                        keepRepeating = false;
                    }

                    students.Add(new Student(id, firstName, lastName));
                    row++;
                }

                return students;
            }
        }
    }
}
