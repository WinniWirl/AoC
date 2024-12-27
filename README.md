# Advent of Code - .Net C# 
<hr>
Ich löse die Rätsel in diesem Projekt mit <b>C#</b>. Warum? Damit ich die Syntax lerne, da ich sie in einem zukünftigen Job benötige.<br>
<br>
Ich habe mir eine kleines Konstrukt gebaut, das den <i>namespace</i> immer an den <b>aktuellen Tag</b> anpasst. Außerdem werden dann automatisch die Inputs heruntergeladen. <br>
<br>
Die Files mit den Lösungen haben folgenden Aufbau: <br>

```C#
using AoC_Day;

namespace AoC2024_DayX
{
    class AOCProgram : AoCDay, ISolvable
    {
        public void SolvePart1()
        {
            throw new NotImplementedException();
        }

        public void SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
```
