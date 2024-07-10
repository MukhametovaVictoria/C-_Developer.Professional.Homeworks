# C-_Developer.Professional.Homeworks

Домашнее задание по принципам SOLID
  
  Краткое описание реализации:
  1. У нас есть классы меню, где указано соотношение "Пункт меню" - "Метод для вызова при выборе этого пункта" (подробнее п.4).
  2. Класс MenuService выполняет переходы по пунктам меню, в завимости от того, какую реализацию IMenu в него передать.
  3. Переходы осуществляются с помощью класса Invoker, который вызывает переданные в него методы.
  4. Реализация IMenu принимает на вход BaseSettings, а точнее любую конкретную реализацию BaseSettings. В данном случае это класс Settings.
     IMenu - это что-то вроде объекта справочника. Он хранит отношение "Пункт меню" - "Метод для вызова при выборе этого пункта".
  
  Итого: можно создать любой класс от BaseSettings с кастомными настройками, затем создать любой класс от IMenu и все это передать в MenuService.

  MenuService может что-то вернуть одним из своих методов, например GetResultByDecision() - получаем результат выполнения метода выбранного пункта меню. Дальше уже решаем что с этим результатом делать.

  Для классов описала какие принципы попыталась реализовать.
  Кратко: 
  1. BaseSettings - Реализация принципа D - Dependency inversion И O - Open/Closed
  2. Invoker - Реализация принципа S -Single responsibility
  3. MenuService - Принципы S, L, D
  4. IMenu - Реализация принципа L - Liskov substitution
  5. IGenerator и ITextWriter - Реализация принципа I - Interface segregation

     Более подробное описание в файлах классов.
     
