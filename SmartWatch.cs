using System;

namespace Lab1_SmartWatch
{
    public class SmartWatch
    {
        // Приватні поля
        private string _modelName;
        private double _price;
        private int _batteryLevel;
        private DateTime _manufactureDate;
        private WatchOS _osType;

        // Властивості
        public Guid Id { get; private set; }
        public bool IsWaterproof { get; set; } = false;

        // Властивості з валідацією
        public string ModelName
        {
            get { return _modelName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва не може бути порожньою.");
                if (value.Length < 1)
                    throw new ArgumentException("Назва має містити хоча б 1 символ.");
                if (value.Length > 20)
                    throw new ArgumentException("Назва не може перевищувати 20 символів.");

                _modelName = value;
            }
        }

        public double Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Ціна не може бути від'ємною.");
                _price = value;
            }
        }

        public int BatteryLevel
        {
            get { return _batteryLevel; }
            set
            {
                if (value < 0 || value > 100)
                    throw new ArgumentException("Заряд батареї має бути в межах 0-100%.");
                _batteryLevel = value;
            }
        }

        public DateTime ManufactureDate
        {
            get { return _manufactureDate; }
            set
            {
                if (value > DateTime.Now)
                    throw new ArgumentException("Дата виготовлення не може бути в майбутньому.");
                if (value.Year < 1800)
                    throw new ArgumentException("Рік виготовлення має бути не менше 1800.");
                _manufactureDate = value;
            }
        }

        public WatchOS OsType
        {
            get { return _osType; }
            set { _osType = value; }
        }

        // Обчислювана властивість
        public string WarrantyStatus
        {
            get
            {
                var ageInDays = (DateTime.Now - ManufactureDate).TotalDays;
                return ageInDays < 365 ? "Active" : "Expired";
            }
        }

        // Конструктор
        public SmartWatch()
        {
            Id = Guid.NewGuid();
            _modelName = "Unknown";
            _price = 0;
            _batteryLevel = 100;
            _manufactureDate = DateTime.Today;
            _osType = WatchOS.Unknown;
        }

        // --- Методи ---

        public void Charge(int minutes)
        {
            Console.WriteLine($"\n[Процес] Заряджаємо {_modelName} протягом {minutes} хвилин...");
            
            int chargeAmount = CalculateChargeAmount(minutes);
            
            if (BatteryLevel + chargeAmount > 100)
                BatteryLevel = 100;
            else
                BatteryLevel += chargeAmount;

            Console.WriteLine($"[Результат] Новий рівень заряду: {BatteryLevel}%");
        }

        public void InstallApp(string appName)
        {
            if (!CanInstallApp())
            {
                Console.WriteLine($"[Помилка] Неможливо встановити '{appName}'. Низький заряд ({BatteryLevel}%).");
            }
            else
            {
                BatteryLevel -= 5;
                Console.WriteLine($"[Успіх] Програма '{appName}' встановлена на {_osType}.");
            }
        }

        public void MakeCall(string phoneNumber)
        {
            Console.WriteLine($"[Дзвінок] Набір номера {phoneNumber} з годинника {_modelName}...");
        }

        public override string ToString()
        {
            return $"ID: {Id.ToString().Substring(0,4)}... | Модель: {ModelName}, ОС: {OsType}, Ціна: {Price}$, Заряд: {BatteryLevel}%";
        }

        // --- Допоміжні методи ---
        
        private int CalculateChargeAmount(int minutes)
        {
            if (minutes < 0) return 0;
            return minutes / 2;
        }

        private bool CanInstallApp()
        {
            const int minBatteryForInstall = 10;
            return BatteryLevel >= minBatteryForInstall;
        }
    }
}