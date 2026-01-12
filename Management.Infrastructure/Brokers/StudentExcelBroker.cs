using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Management.Application.Interfaces;
using Management.Domain.Models;
using OfficeOpenXml;

namespace Management.Infrastructure.Brokers
{
    public class StudentExcelBroker : IStudentExcelBroker
    {
        public void Export(List<Student> students)
        {
            try
            {
                var filePath = $"Students_{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}.xlsx";

                var fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                    fileInfo.Create().Close();

                using var package = new ExcelPackage(fileInfo);
                var sheet = package.Workbook.Worksheets.Add("Students");

                // Labels
                sheet.Cells[1, 1].LoadFromText("ID");
                sheet.Cells[1, 2].LoadFromText("First Name");
                sheet.Cells[1, 3].LoadFromText("Last Name");

                // Load all students
                sheet.Cells[2, 1].LoadFromCollection(students);

                // Save all changes
                package.Save();
            }
            catch (Exception e)
            {
                Console.WriteLine("Excel Error: {0}", e.Message);
                throw;
            }
        }

        public List<Student> Import(string filePath)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);

                if (!fileInfo.Exists)
                {
                    throw new FileNotFoundException("Excel file not found", fileInfo.FullName);
                }

                List<Student> students = [];

                using var package = new ExcelPackage(fileInfo);

                if (package.Workbook.Worksheets.Count < 1)
                    throw new Exception("The Excel file has no sheets");

                var sheet = package.Workbook.Worksheets.First();

                var keepRepeating = true;
                var row = 2;

                do
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
                } while (keepRepeating);

                return students;
            }
            catch (Exception e)
            {
                Console.WriteLine("Excel Error: {0}", e.Message);
                throw;
            }
        }
    }
}
