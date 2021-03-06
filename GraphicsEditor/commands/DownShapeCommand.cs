﻿using System;
using ConsoleUI;

namespace GraphicsEditor
{
    public class DownShapeCommand : ICommand
    {
        private readonly Picture picture;
        
        public DownShapeCommand(Picture picture)
        {
            this.picture = picture;
        }

        public string Name => "down";
        public string Help => "Опустить фигурy";
        public string Description => "down shape1 [shape2 ...]";
        public string[] Synonyms => new string[0];

        public void Execute(params string[] parameters)
        {
            if (parameters.Length < 1)
            {
                Console.WriteLine($"Неверное количество параметров: {parameters.Length}");
                return;
            }

            try
            {
                picture.Move(1, parameters);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Невозможно опустить: {exception.Message}");
            }
        }
    }
}