//Антон и Ваня друзья
﻿//Hbyf Dbrf
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary7
{
    public class Participant
    {
        // Свойства класса
        public string Name { get; private set; }
        public double Distance { get; private set; }
        public double Time { get; private set; }
        public double Speed => Distance / Time; // cкорость, вычисляемая на основе дистанции и времени

        // Конструктор класса
        public Participant(string name, double distance, double time)
        {
            Name = name;
            Distance = distance;
            Time = time;
        }

        // Метод для сохранения списка участников в файл
        public static void SaveToFile(List<Participant> participants, string fileName)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    sw.WriteLine(participants[0].Distance); // записываем дистанцию (одинаковую для всех участников)
                    sw.WriteLine(participants.Count); // записываем количество участников
                    foreach (var participant in participants) // записываем данные каждого участника
                    {
                        sw.WriteLine(participant.Name);
                        sw.WriteLine(participant.Time);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка доступа к файлу: " + ex.Message);
            }
        }

        // Метод для загрузки списка участников из файла
        public static List<Participant> LoadFromFile(string fileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(fileName, Encoding.UTF8))
                {
                    double distance = double.Parse(sr.ReadLine());
                    int count = int.Parse(sr.ReadLine());
                    List<Participant> participants = new List<Participant>();

                    for (int i = 0; i < count; i++)
                    {
                        string name = sr.ReadLine();
                        double time = double.Parse(sr.ReadLine());
                        participants.Add(new Participant(name, distance, time));
                    }

                    return participants;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка доступа к файлу: " + ex.Message);
            }
        }
    }
}
