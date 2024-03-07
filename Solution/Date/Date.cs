using System;
using System.Linq;

namespace Date
{
    /// <summary>
    /// Это простая библиотека классов под названием 'Date'.
    /// Она имеет следующие функции:
    /// --) Установка/получение даты (год, месяц, день)
    /// --) Определение високосного года
    /// --) Определение количества дней в месяце
    /// --) Получение названия месяца по его номеру/индексу
    /// --) Получение номера/индекса месяца по его названию
    /// </summary>
    public class Date
    {
        // Приватные поля для года, месяца и дня
        private int _year, _month, _day;

        // Массив месяцев с 30 днями
        private static byte[] MonthWith30days = { 3, 5, 8, 10 };

        // Массив названий месяцев
        private static string[] monthNames =
        {
            "Junuary", "February", "March", "April",
            "May", "June", "July", "August", "September",
            "October", "November", "December"
        };

        /// <summary>
        /// Свойство для Года. Значение должно быть неотрицательным.
        /// </summary>
        public int Year
        {
            get { return _year; } // Возвращает значение года

            set
            {
                if (value < 0) // Если значение меньше 0
                {
                    throw new Exception("Недопустимый аргумент: введенное значение меньше нуля (не положительное)");
                }
                else
                {
                    _year = value; // Устанавливает значение года
                }
            }
        }

        /// <summary>
        /// Свойство для Месяца.
        /// Если значение от 0 до 11, оно устанавливает номер месяца.
        /// Если значение больше 11, оно добавляет месяцы.
        /// Если значение меньше 0, оно вычитает месяцы.
        /// </summary>
        public int Month
        {
            get { return _month; } // Возвращает значение месяца

            set
            {
                if (AllMonths + value < 0) // Если общее количество месяцев плюс значение меньше 0
                {
                    throw new Exception("Недопустимый аргумент: значение месяца было меньше нуля, а значение года равно нулю");
                }
                else if (value < 12) // Если значение меньше 12
                {
                    _month = value; // Устанавливает значение месяца

                    while (_month < 0) // Пока значение месяца меньше 0
                    {
                        _year--; // Уменьшает год на 1
                        _month += 12; // Увеличивает месяц на 12
                    }
                }
                else // Если значение больше или равно 12
                {
                    _year += value / 12; // Увеличивает год на значение, деленное на 12
                    _month = value % 12; // Устанавливает месяц равным остатку от деления значения на 12
                }

                if (_day > DaysAmount) // Если день больше количества дней в месяце
                {
                    _day -= DaysAmount; // Уменьшает день на количество дней в месяце
                    Month++; // Увеличивает месяц на 1
                }
            }
        }

        /// <summary>
        /// Свойство для Дня.
        /// Если значение от 0 до количества дней в месяце, оно устанавливает номер дня.
        /// Если значение больше количества дней в месяце, оно добавляет дни.
        /// Если значение меньше 0, оно вычитает дни.
        /// </summary>
        public int Day
        {
            get { return _day; } // Возвращает значение дня

            set
            {
                if (AllDays + value < 0) // Если общее количество дней плюс значение меньше 0
                {
                    throw new Exception("Недопустимый аргумент: значение дня было меньше нуля, и общее количество дней недостаточно");
                }
                else if (value < DaysAmount) // Если значение меньше количества дней в месяце
                {
                    _day = value; // Устанавливает значение дня

                    while (_day < 0) // Пока значение дня меньше 0
                    {
                        Month--; // Уменьшает месяц на 1
                        _day += DaysAmount; // Увеличивает день на количество дней в месяце
                    }

                    if (_day > DaysAmount) // Если день больше количества дней в месяце
                    {
                        _day -= DaysAmount; // Уменьшает день на количество дней в месяце
                        Month++; // Увеличивает месяц на 1
                    }
                }
                else // Если значение больше или равно количеству дней в месяце
                {
                    _day = value; // Устанавливает значение дня

                    while (_day > DaysAmount) // Пока день больше количества дней в месяце
                    {
                        _day -= DaysAmount; // Уменьшает день на количество дней в месяце
                        Month++; // Увеличивает месяц на 1
                    }
                }
            }
        }

        /// <summary>
        /// Свойство для проверки, является ли год високосным.
        /// Возвращает true, если год является високосным, в противном случае возвращает false.
        /// </summary>
        public bool YearLeapness
        {
            get
            {
                return (_year % 4 == 0 && _year % 100 != 0) || _year % 400 == 0; // Проверяет, является ли год високосным
            }
        }

        /// <summary>
        /// Конструктор для класса Date.
        /// </summary>
        public Date(int year = 0, int month = 0, int day = 0)
        {
            Year = year; // Устанавливает значение года
            Month = month; // Устанавливает значение месяца
            Day = day; // Устанавливает значение дня
        }

        /// <summary>
        /// Метод для создания копии объекта Date.
        /// </summary>
        public Date Copy()
        {
            return new Date(_year, _month, _day); // Возвращает новый объект Date с теми же значениями года, месяца и дня
        }

        /// <summary>
        /// Свойство для получения количества дней в текущем месяце.
        /// </summary>
        public int DaysAmount
        {
            get
            {
                if (Array.Exists(MonthWith30days, element => element == _month)) // Если текущий месяц содержится в массиве месяцев с 30 днями
                {
                    return 30; // Возвращает 30
                }
                else if (_month == 1) // Если текущий месяц - февраль
                {
                    if (YearLeapness) // Если год високосный
                    {
                        return 29; // Возвращает 29
                    }
                    else
                    {
                        return 28; // В противном случае возвращает 28
                    }
                }
                else
                {
                    return 31; // Возвращает 31 для всех остальных м
                               // Возвращает 31 для всех остальных месяцев
                }
            }
        }

        /// <summary>
        /// Свойство для получения общего количества месяцев.
        /// </summary>
        public int AllMonths
        {
            get
            {
                return _year * 12 + _month; // Возвращает общее количество месяцев, умножая год на 12 и добавляя месяц
            }
        }

        /// <summary>
        /// Свойство для получения общего количества дней.
        /// </summary>
        public uint AllDays
        {
            get
            {
                uint res = 0; // Инициализирует результат как 0

                int y = _year; // Создает копию года
                int m = _month; // Создает копию месяца
                int d = _day; // Создает копию дня

                while (y > 0) // Пока год больше 0
                {
                    res += (uint)DaysAmountInYear; // Увеличивает результат на количество дней в году
                    y--; // Уменьшает год на 1
                }

                while (m > 0) // Пока месяц больше 0
                {
                    res += (uint)DaysAmount; // Увеличивает результат на количество дней в месяце
                    m--; // Уменьшает месяц на 1
                }

                return res + (uint)d; // Возвращает результат плюс день
            }
        }

        /// <summary>
        /// Свойство для получения количества дней в текущем году.
        /// </summary>
        public int DaysAmountInYear
        {
            get
            {
                if (YearLeapness) // Если год високосный
                {
                    return 366; // Возвращает 366
                }
                else
                {
                    return 365; // В противном случае возвращает 365
                }
            }
        }

        /// <summary>
        /// Свойство для получения названия текущего месяца.
        /// </summary>
        public string MonthName
        {
            get
            {
                return monthNames[_month]; // Возвращает название месяца по индексу
            }
        }

        /// <summary>
        /// Метод для получения номера месяца по его названию.
        /// Возвращает индекс месяца в диапазоне от 1 до 12 (0, если не найдено).
        /// </summary>
        public static int GetMonthNumberByMonthName(string monthname)
        {
            return Array.IndexOf(monthNames.Select(c => c.ToLower()).ToArray<string>(), monthname.ToLower()); // Возвращает индекс месяца по его названию
        }

        /// <summary>
        /// Свойство для получения полной информации о дате в виде строки.
        /// Пример: "Год: 2002   Месяц: Ноябрь     День: 12"
        /// </summary>
        public string DateInfo
        {
            get
            {
                return $"Год: {_year}  Месяц: {MonthName}  День: {_day + 1}"; // Возвращает строку с информацией о дате
            }
        }

        #region Операторы

        public static Date operator +(Date d1, Date d2)
        {
            return new Date(d1.Year + d2.Year, d1.Month + d2.Month, d1.Day + d2.Day); // Возвращает новый объект Date, складывая год, месяц и день двух дат
        }

        public static Date operator -(Date d1, Date d2)
        {
            return new Date(d1.Year - d2.Year, d1.Month - d2.Month, d1.Day - d2.Day); // Возвращает новый объект Date, вычитая год, месяц и день одной даты из другой
        }

        public static Date operator ++(Date date)
        {
            date.Day++; // Увеличивает день на 1
            return date; // Возвращает дату
        }

        public static Date operator --(Date date)
        {
            date.Day--; // Уменьшает день на 1
            return date; // Возвращает дату
        }

        public static bool operator ==(Date d1, Date d2)
        {
            return (d1._year == d2._year &&
                d1._month == d2._month &&
                d1._day == d2._day); // Возвращает true, если год, месяц и день двух дат равны
        }

        public static bool operator !=(Date d1, Date d2)
        {
            return (d1._year != d2._year ||
                d1._month != d2._month ||
                d1._day != d2._day); // Возвращает true, если год, месяц или день двух дат не равны
        }

        public static bool operator >(Date d1, Date d2)
        {
            return
                (d1._year > d2._year) ||
                (d1._year == d2._year && d1._month > d2._month) ||
                (d1._year == d2._year && d1._month == d2._month && d1._day > d2._day); // Возвращает true, если первая дата больше второй
        }

        public static bool operator <(Date d1, Date d2)
        {
            return !(d1 > d2) && d1 != d2; // Возвращает true, если первая дата меньше второй
        }

        public static bool operator >=(Date d1, Date d2)
        {
            return !(d1 < d2); // Возвращает true, если первая дата больше или равна второй
        }

        public static bool operator <=(Date d1, Date d2)
        {
            return !(d1 > d2); // Возвращает true, если первая дата меньше или равна второй
        }

        public static bool operator false(Date date)
        {
            return date == null ||
                (date._year == 0 &&
                date._month == 0 &&
                date._day == 0); // Возвращает true, если дата равна null или год, месяц и день равны 0
        }

        public static bool operator true(Date date)
        {
            return
                date._year != 0 ||
                date._month != 0 ||
                date._day != 0; // Возвращает true, если год, месяц или день не равны 0
        }

        #endregion
    }
}