Module Module1
#Region "Global Variable Declaration"
    Dim rnd As System.Random = New System.Random()
    Dim TargetName As String
    Dim t As Integer
    Dim mode As Integer
    Dim n As Integer
    Dim PlayAgain As Integer = 1
    Dim Player1 As New Hero
    Dim monst As New Enemy
    Dim Monster1 As New Enemy
    Dim monster2 As New Enemy
    Dim monster3 As New Enemy
    Dim monster4 As New Enemy
    Dim Story As New StoryMode
    Dim StatusSetter As New StatusSetting
    Dim Level As New LevelSystem
    Dim user As String
    Dim ref As New Abilities
    Dim File As New SaveLoad
    Dim Progress As Integer = 0
    Dim z As Integer
    Dim background As New System.Media.SoundPlayer
    Dim music As SByte
#End Region
#Region "Classes"
    Class Abilities
#Region "Ability Variables"
        Dim r As Integer
        Public healnumber As Integer
#End Region
        Sub AbilityPanel()
            Dim y As Integer = -1
            While y = -1
                Select Case (Player1.ClassChoice)
                    Case 1
                        Console.WriteLine("0. Back")
                        Console.WriteLine("1. Rending Slash Mp: 1")
                        Console.WriteLine("2. Charge Attack Mp: 1")
                        Try
                            y = Console.ReadLine
                        Catch ex As Exception
                            y = -1
                        End Try
                        If y = 1 Then
                            ref.RendingSlash(Player1.power)
                        End If
                        If y = 0 Then
                            Player1.MainPanel()
                        End If
                        If y = 2 Then
                            ref.ChargeUp()
                        End If
                        If y < 0 Or y > 2 Then
                            y = -1
                        End If
                    Case 2
                        Console.WriteLine("0. Back")
                        Console.WriteLine("1. Fireball      Mp: 2")
                        Console.WriteLine("2. Heal          Mp: 1")
                        Try
                            y = Console.ReadLine
                        Catch ex As Exception
                            y = -1
                        End Try
                        If y = 1 Then
                            ref.FireBall(Player1.EleMagic)
                        End If
                        If y = 0 Then
                            Player1.MainPanel()
                        End If
                        If y = 2 Then
                            ref.heal(Player1.HolyMagic)
                            Player1.health = Player1.health + healnumber
                        End If
                        If y < 0 Or y > 2 Then
                            y = -1
                        End If
                    Case 3
                        Console.WriteLine("0. Back")
                        Console.WriteLine("1. Heal          Mp: 1")
                        Console.WriteLine("2. Heal+         Mp: 2")
                        Console.WriteLine("3. Smite         Mp: 1")
                        Try
                            y = Console.ReadLine
                        Catch ex As Exception
                            y = -1
                        End Try
                        If y = 1 Then
                            ref.heal(Player1.HolyMagic)
                            Player1.health = Player1.health + healnumber
                        End If
                        If y = 2 Then
                            ref.healplus(Player1.HolyMagic)
                            Player1.health = Player1.health + healnumber
                        End If
                        If y = 3 Then
                            ref.smite(Player1.HolyMagic)
                        End If
                        If y = 0 Then
                            Console.WriteLine()
                            Player1.MainPanel()
                        End If
                        If y < 0 Or y > 3 Then
                            y = -1
                        End If
                    Case 4
                        Console.WriteLine("0. Back")
                        Console.WriteLine("1. Rending Slash Mp: 1")
                        Console.WriteLine("2. Heal          Mp: 1")
                        Try
                            y = Console.ReadLine
                        Catch ex As Exception
                            y = -1
                        End Try
                        If y = 1 Then
                            ref.RendingSlash(Player1.power)
                        End If
                        If y = 2 Then
                            heal(Player1.HolyMagic)
                            Player1.health = Player1.health + healnumber
                        End If
                        If y = 0 Then
                            Player1.MainPanel()
                        End If
                        If y < 0 Or y > 2 Then
                            y = -1
                        End If
                    Case 100
                        Console.WriteLine("0. Back")
                        Console.WriteLine("1. Nature's Blessing")
                        Try
                            y = Console.ReadLine
                        Catch ex As Exception
                            y = -1
                        End Try
                        If y = 0 Then
                            Player1.MainPanel()
                        ElseIf y = 1 Then
                            NaturesBlessing(Player1.HolyMagic)
                            Player1.health = Player1.health + healnumber
                        Else
                            y = -1
                        End If
                        If y < 0 Or y > 1 Then
                            y = -1
                        End If
                End Select
                If y = -1 Then
                    ErrorMessage()
                    Console.WriteLine()
                End If
            End While
            If Player1.health > Player1.MaxHealth Then
                Player1.health = Player1.MaxHealth
            End If
        End Sub
        Sub CheckMana(ByVal x)
            z = 0
            If user = Player1.name Then
                If Player1.mana >= x Then
                    Player1.mana = Player1.mana - x
                    z = 1
                    r = 1
                Else
                    Console.WriteLine("Not Enough Mana!")
                    AbilityPanel()
                End If
            Else
                If user = Monster1.Name Then
                    Monster1.Mana = Monster1.Mana - x
                    r = 1
                End If
                If user = monster2.Name Then
                    monster2.Mana = monster2.Mana - x
                    r = 1
                End If
                If user = monster3.Name Then
                    monster3.Mana = monster3.Mana - x
                    r = 1
                End If
                If user = monster4.Name Then
                    monster4.Mana = monster4.Mana - x
                    r = 1
                End If
            End If
        End Sub
        Sub FireBall(ByVal x)
            Dim temp
            CheckMana(2)
            If r = 1 Then
                Player1.ChooseTarget()
                temp = x * 2 + rnd.Next(-2, 3)
                Console.ForegroundColor = ConsoleColor.Magenta
                Console.Write(user)
                Console.Write(" conjures a mighty fireball, and launches it at the ")
                Console.Write(TargetName)
                Console.WriteLine("!")
                temp = CriticalHit(temp)
                If user = Player1.name Then
                    monst.TakeDamage(temp)
                Else
                    Player1.TakeDamage(temp)
                End If
                Console.ForegroundColor = ConsoleColor.DarkBlue
                r = 0
            End If
        End Sub
        Sub heal(ByVal x)
            CheckMana(1)
            If r = 1 Then
                healnumber = 4 + x + rnd.Next(-2, 5)
                Console.ForegroundColor = ConsoleColor.DarkGreen
                Console.Write(user)
                Console.WriteLine("'s Health Has Been Replenished!")
                Console.WriteLine(healnumber)
                Console.ForegroundColor = ConsoleColor.DarkBlue
                r = 0
            End If
        End Sub
        Sub healplus(ByVal x)
            CheckMana(2)
            If r = 1 Then
                healnumber = 4 + x + rnd.Next(-2, 4)
                Console.ForegroundColor = ConsoleColor.DarkGreen
                Console.Write(user)
                Console.WriteLine("'s Health Has Been Replenished!")
                Console.WriteLine(healnumber)
                Console.ForegroundColor = ConsoleColor.DarkBlue
                r = 0
            End If
        End Sub
        Sub RendingSlash(ByVal x)
            Dim temp As Integer
            CheckMana(1)
            If r = 1 Then
                If user = Player1.name Then
                    Player1.ChooseTarget()
                End If
                Console.Write(user)
                Console.Write(" raises his weapon and rips through ")
                If user = Player1.name Then
                    Console.WriteLine("the ")
                End If
                Console.WriteLine(TargetName)
                temp = (x * 1.5 + rnd.Next(-2, 4))
                CriticalHit(temp)
                r = 0
            End If
        End Sub
        Sub NaturesBlessing(ByVal x)
            healnumber = x
            Console.Write("The ")
            Console.Write(user)
            Console.WriteLine(" is mysteriously mended")
            Console.WriteLine(healnumber)
        End Sub
        Sub ChargeUp()
            CheckMana(1)
            If r = 1 Then
                If user = Player1.name Then
                    Player1.ChooseTarget()
                End If
                Console.ForegroundColor = ConsoleColor.Yellow
                If user <> Player1.name Then
                    Console.Write("The ")
                End If
                Console.Write(user)
                Console.WriteLine(" gathers up their power")
                Console.WriteLine()
                If user = Player1.name Then
                    Player1.charge = 1
                Else
                    If user = Monster1.Name Then
                        Monster1.charge = 1
                    End If
                    If user = monster2.Name Then
                        monster2.charge = 1
                    End If
                    If user = monster3.Name Then
                        monster3.charge = 1
                    End If
                    If user = monster4.Name Then
                        monster4.charge = 1
                    End If
                End If
                Console.ForegroundColor = ConsoleColor.DarkBlue
                r = 0
            End If
        End Sub
        Sub ChargeAttack(ByVal x)
            Dim temp As Integer
            Console.ForegroundColor = ConsoleColor.Red
            Console.Write("The ")
            Console.Write(user)
            Console.WriteLine(" strikes powerfully!")
            temp = x * 2.5 + rnd.Next(1, 4)
            temp = CriticalHit(temp)
            Console.Write(TargetName)
            Console.WriteLine(" suffers a bone crushing blow!")

            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            If user = Player1.name Then
                Player1.charge = 0
            End If
            If user = Monster1.Name Then
                Monster1.charge = 0
            End If
            If user = monster2.Name Then
                monster2.charge = 0
            End If
            If user = monster3.Name Then
                monster3.charge = 0
            End If
            If user = monster4.Name Then
                monster4.charge = 0
            End If
        End Sub
        Sub Smite(ByVal x As Integer)
            Dim temp As Integer
            CheckMana(1)
            If r = 1 Then
                If user = Player1.name Then
                    Player1.ChooseTarget()
                End If
                temp = x * 1.6 + rnd.Next(-2, 3)
                If user <> Player1.name Then
                    Console.Write("The ")
                End If
                Console.Write(user & " strikes ")
                If user <> Player1.name Then
                    Console.Write("the ")
                End If
                Console.WriteLine(TargetName & " with holy lightning!")
                temp = CriticalHit(temp)
                If user = Player1.name Then
                    monst.TakeDamage(temp)
                Else
                    Player1.TakeDamage(temp)
                End If
            End If
        End Sub
    End Class
    Class Enemy
#Region "Enemy Variable Declaration"
        Public Name As String
        Public Health As Integer = 0
        Public MaxHealth As Integer
        Public HolyMagic As Integer
        Public EleMagic As Integer
        Public Mana As Integer
        Public Power As Integer
        Public MaxMana As Integer
        Public Defence As Integer
        Public charge As Integer
        Public MCost As Integer
        Public y As Integer
        Public HealNumber As Integer
        Public CriticalChance As Integer
        Public CanAttack As Integer
        Public CanHeal As Integer
        Public CanChargeUp As Integer
        Public MadeMove As Integer
        Public CanNaturesBlessing As Integer = 0
        Public NumberOfEnemies As Integer
        Public dead As Boolean = True
        Public exp As Integer = 0
        Public Type As Integer
#End Region
        Sub TakeDamage(ByVal x As Integer)
            Dim temp As Integer
            Select Case (Player1.Target)
                Case 1
                    temp = x - Monster1.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    Monster1.Health = Monster1.Health - temp
                Case 2
                    temp = x - monster2.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    monster2.Health = monster2.Health - temp
                Case 3
                    temp = x - monster3.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    Monster1.Health = monster3.Health - temp
                Case 4
                    temp = x - monster4.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    monster4.Health = monster4.Health - temp
                Case 5
                    temp = x - Monster1.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    Monster1.Health = Monster1.Health - temp
                    temp = x - monster2.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    monster2.Health = monster2.Health - temp
                    temp = x - monster3.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    Monster1.Health = monster3.Health - temp
                    temp = x - monster4.Defence
                    If temp < 1 Then
                        temp = 0
                    End If
                    Console.WriteLine(temp)
                    monster4.Health = monster4.Health - temp
            End Select
        End Sub
        Function CriticalHit(ByVal x As Integer)
            Dim y As Integer
            y = rnd.Next(1, 101)
            If y <= CriticalChance Then
                Console.WriteLine("Critical Hit!")
                x = x * 1.5
            End If
            Return x
        End Function
        Sub Attack()
            Dim temp As Integer
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.Write("The ")
            Console.Write(user)
            Console.WriteLine(" strikes!")
            temp = Power + rnd.Next(-2, 3)
            temp = CriticalHit(temp)
            Console.Write(Player1.name)
            Console.WriteLine(" takes a hit!")
            Player1.TakeDamage(temp)
            Console.ForegroundColor = ConsoleColor.DarkBlue
        End Sub
        Sub Heal()
            Mana = Mana - 1
            HealNumber = (HolyMagic + rnd.Next(-2, 3))
            Health = Health + HealNumber
            If Health > MaxHealth Then
                Health = MaxHealth
            End If
            Console.ForegroundColor = ConsoleColor.DarkYellow
            Console.WriteLine("The enemy heals itself")
            Console.WriteLine(HealNumber)
            Console.ForegroundColor = ConsoleColor.DarkBlue
        End Sub
        Sub Turn()
            Dim z As Integer
            z = rnd.Next(1, 4)
            If CanNaturesBlessing = 1 And y = 3 Then
                ref.NaturesBlessing(HolyMagic)
                MadeMove = 1
            End If
            If charge = 0 Then
                If Mana > 0 And Health < 10 And CanHeal = 1 Then
                    ref.heal(HolyMagic)
                    Health = ref.healnumber + Health
                    MadeMove = 1
                ElseIf y = z And Mana > 0 And CanChargeUp = 1 Then
                    ref.ChargeUp()
                    MadeMove = 1
                Else
                    If CanAttack = 1 Then
                        Attack()
                        MadeMove = 1
                    End If
                End If
            Else
                ref.ChargeAttack(Power)
                MadeMove = 1
            End If
            If MadeMove = 0 Then
                DoNothing()
            End If
            y += 1
        End Sub
        Sub DoNothing()
            Console.Write("The ")
            Console.Write(user)
            Console.WriteLine(" glares menecingly, but takes no action")
            Console.WriteLine()
        End Sub
        Sub SetStats(ByVal x)
            Select Case (x)
                Case 0
                    Name = "Training Dummy"
                    MaxHealth = 50
                    Power = 0
                    HolyMagic = 999
                    MaxMana = 0
                    Defence = 15
                    CriticalChance = 0
                    CanHeal = 0
                    CanAttack = 0
                    CanChargeUp = 0
                    CanNaturesBlessing = 1
                Case 1
                    Name = "Rabid Chicken"
                    MaxHealth = 30
                    Power = 4
                    HolyMagic = 3
                    MaxMana = 4
                    Defence = 0
                    CriticalChance = 8
                    CanAttack = 1
                    CanHeal = 1
                    exp = 6
                Case 2
                    Name = "Black Eagle"
                    MaxHealth = 25
                    Power = 3
                    MaxMana = 3
                    Defence = 0
                    CriticalChance = 5
                    CanAttack = 1
                    exp = 3
                Case 3
                    Name = "Burrowing Screecher"
                    MaxHealth = 20
                    Power = 4
                    MaxMana = 2
                    Defence = 0
                    CriticalChance = 20
                    CanAttack = 1
                    CanChargeUp = 1
                    exp = 5
                Case 31
                    Name = "Ogre"
                    MaxHealth = 35
                    Power = 5
                    HolyMagic = 6
                    MaxMana = 6
                    Defence = 0
                    CriticalChance = 15
                    CanAttack = 1
                    CanChargeUp = 1
                    CanHeal = 1
                    exp = 7
                Case 61
                    Name = "Giant"
                    MaxHealth = 45
                    Power = 6
                    HolyMagic = 8
                    MaxMana = 8
                    Defence = 1
                    CriticalChance = 22
                    CanAttack = 1
                    CanChargeUp = 1
                    CanHeal = 1
                    exp = 10
                Case 100
                    Name = "Black Chicken of Death"
                    MaxHealth = 40
                    Power = 5
                    MaxMana = 5
                    Defence = 6
                    CriticalChance = 5
                    CanAttack = 1
                    CanChargeUp = 1
                    exp = 50
                Case Else
                    ErrorMessage()
                    ChooseEnemy()
            End Select
        End Sub
    End Class
    Class Hero
#Region "Hero Variable Declaration"
        Public ClassChoice As Integer
        Public charge As Integer
        Public name As String
        Public health As Integer
        Public MaxHealth As Integer
        Public HolyMagic As Integer
        Public mana As Integer
        Public MaxMana As Integer
        Public power As Integer
        Public choice As Integer
        Public defence As Integer
        Public EleMagic As Integer
        Public x As Integer
        Public HBar As Double
        Public MBar As Integer
        Public MCost As Integer
        Public prefix As String
        Public HeroClass As String
        Public CriticalChance As Double
        Public Target As Integer = 1
        Public dead As Boolean = False
#End Region
        Sub ChooseTarget()
            Dim y As Integer = 1
            Dim l As Integer
            y = 1
            While y = 1
                y = 0
                While l = 0
                    l = 1
                    Console.WriteLine("Select a target: ")
                    If Monster1.dead = 0 Then
                        Console.Write("1. ")
                        Console.Write(Monster1.Name)
                        Console.Write(" ")
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.Write(Monster1.Health)
                        If Monster1.Health >= 10 Then
                            Console.Write(" / ")
                        Else
                            Console.Write("  / ")
                        End If
                        Console.Write(Monster1.MaxHealth)
                        Console.Write(" ")
                        HBar = Monster1.Health / 20
                        While HBar <= Monster1.Health
                            Console.Write("|")
                            HBar += Monster1.MaxHealth / 20
                        End While
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        While HBar <= Monster1.MaxHealth
                            Console.Write("|")
                            HBar += Monster1.MaxHealth / 20
                        End While
                        Console.WriteLine()
                    End If
                    Console.ForegroundColor = ConsoleColor.DarkBlue
                    If monster2.dead = 0 Then
                        Console.Write("2. ")
                        Console.Write(monster2.Name)
                        Console.Write(" ")
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.Write(monster2.Health)
                        If monster2.Health >= 10 Then
                            Console.Write(" / ")
                        Else
                            Console.Write("  / ")
                        End If
                        Console.Write(monster2.MaxHealth)
                        Console.Write(" ")
                        HBar = monster2.Health / 20
                        While HBar <= monster2.Health
                            Console.Write("|")
                            HBar += monster2.MaxHealth / 20
                        End While
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        While HBar <= monster2.MaxHealth
                            Console.Write("|")
                            HBar += monster2.MaxHealth / 20
                        End While
                        Console.WriteLine()
                    End If
                    Console.ForegroundColor = ConsoleColor.DarkBlue
                    If monster3.dead = 0 Then
                        Console.Write("3. ")
                        Console.Write(monster3.Name)
                        Console.Write(" ")
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.Write(monster3.Health)
                        If monster3.Health >= 10 Then
                            Console.Write(" / ")
                        Else
                            Console.Write("  / ")
                        End If
                        Console.Write(monster3.MaxHealth)
                        Console.Write(" ")
                        HBar = monster3.Health / 20
                        While HBar <= monster3.Health
                            Console.Write("|")
                            HBar += monster3.MaxHealth / 20
                        End While
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        While HBar <= monster3.MaxHealth
                            Console.Write("|")
                            HBar += monster3.MaxHealth / 20
                        End While
                        Console.WriteLine()
                    End If
                    Console.ForegroundColor = ConsoleColor.DarkBlue
                    If monster4.dead = 0 Then
                        Console.Write("4. ")
                        Console.Write(monster4.Name)
                        Console.Write(" ")
                        Console.ForegroundColor = ConsoleColor.DarkRed
                        Console.Write(monster4.Health)
                        If monster4.Health >= 20 Then
                            Console.Write(" / ")
                        Else
                            Console.Write("  / ")
                        End If
                        Console.Write(monster4.MaxHealth)
                        Console.Write(" ")
                        HBar = monster4.Health / 20
                        While HBar <= monster4.Health
                            Console.Write("|")
                            HBar += monster4.MaxHealth / 20
                        End While
                        Console.ForegroundColor = ConsoleColor.DarkGray
                        While HBar <= monster4.MaxHealth
                            Console.Write("|")
                            HBar += monster4.MaxHealth / 20
                        End While
                        Console.WriteLine()
                        Console.ForegroundColor = ConsoleColor.DarkBlue
                    End If
                    Try
                        Target = Console.ReadLine
                    Catch ex As Exception
                        Target = 0
                    End Try
                    If Target = 0 Or Target > 4 Then
                        ErrorMessage()
                        l = 0
                    End If
                End While
                Select Case (Target)
                    Case 1
                        TargetName = Monster1.Name
                    Case 2
                        TargetName = monster2.Name
                    Case 3
                        TargetName = monster3.Name
                    Case 4
                        TargetName = monster4.Name
                End Select
                Select Case (Player1.Target)
                    Case 1
                        If Monster1.dead = 1 Then
                            Console.WriteLine("Invalid Target")
                            Player1.ChooseTarget()
                            l = 0
                        Else
                            l = 1
                        End If
                    Case 2
                        If monster2.dead = 1 Then
                            Console.WriteLine("Invalid Target")
                            Player1.ChooseTarget()
                            l = 0
                        Else
                            l = 1
                        End If
                    Case 3
                        If monster3.dead = 1 Then
                            Console.WriteLine("Invalid Target")
                            Player1.ChooseTarget()
                            l = 0
                        Else
                            l = 1
                        End If
                    Case 4
                        If monster4.dead = 1 Then
                            Console.WriteLine("Invalid Target")
                            Player1.ChooseTarget()
                            l = 0
                        Else
                            l = 1
                        End If
                End Select
            End While
        End Sub
        Sub SetStats(ByVal x)
            Select Case (x)
                Case 1
                    HeroClass = "Warrior"
                    prefix = "Warrior "
                    MaxHealth = 30
                    HolyMagic = 0
                    EleMagic = 0
                    MaxMana = 3
                    power = 6
                    defence = 2
                    CriticalChance = 15
                Case 2
                    HeroClass = "Mage"
                    prefix = "Mage "
                    MaxHealth = 20
                    HolyMagic = 4
                    EleMagic = 8
                    MaxMana = 9
                    power = 4
                    defence = 0
                    CriticalChance = 20
                Case 3
                    HeroClass = "Priest"
                    prefix = "Priest "
                    MaxHealth = 25
                    HolyMagic = 6
                    EleMagic = 2
                    MaxMana = 6
                    power = 3
                    defence = 1
                    CriticalChance = 10
                Case 4
                    HeroClass = "Ranger"
                    prefix = "Ranger "
                    MaxHealth = 30
                    HolyMagic = 2
                    EleMagic = 4
                    MaxMana = 4
                    power = 5
                    defence = 1
                    CriticalChance = 20
                Case 100
                    HeroClass = "Boss"
                    prefix = "Boss "
                    MaxHealth = 50
                    HolyMagic = 50
                    power = 50
                    defence = 15
                    CriticalChance = 30
            End Select
        End Sub
        Sub TakeDamage(ByVal x As Integer)
            x = x - defence
            If x < 0 Then
                x = 0
            End If
            Console.WriteLine(x)
            health = health - x
        End Sub
        Sub Attack()
            Dim temp As Integer
            ChooseTarget()
            Console.ForegroundColor = ConsoleColor.DarkMagenta
            Console.Write(name)
            Console.WriteLine(" Attacks!")
            temp = power + rnd.Next(-2, 3)
            temp = CriticalHit(temp)
            monst.TakeDamage(temp)
            Console.ForegroundColor = ConsoleColor.DarkBlue
        End Sub
        Sub MainPanel()
            Dim y As Integer = 1
            choice = 0
            While choice = 0
                Console.Write("It is ")
                Console.Write(name)
                Console.WriteLine("'s turn!")
                Console.WriteLine("Select an action: 1. attack, 2. abilities or 3. surrender")
                Try
                    choice = System.Console.ReadLine
                Catch ex As Exception
                    choice = 0
                    Console.WriteLine("Invalid Input: Try Again")
                    Console.WriteLine()
                End Try
                Select Case (choice)
                    Case 0

                    Case 1
                        Attack()
                    Case 2
                        ref.AbilityPanel()
                    Case 3
                        health = 0
                    Case Else
                        Console.WriteLine("That is not a valid choice! Try Again")
                        Console.WriteLine()
                        choice = 0
                End Select
            End While
        End Sub
        Sub Turn()
            Console.Write("Level: ")
            Console.WriteLine(Level.level)
            Console.ForegroundColor = ConsoleColor.DarkGreen
            Console.Write("Health: ")
            HBar = (health / 25)
            Console.Write(health)
            If health >= 10 Then
                Console.Write(" / ")
            Else
                Console.Write("  / ")
            End If
            Console.Write(MaxHealth)
            Console.Write(" ")
            While HBar <= health
                Console.Write("|")
                HBar += (MaxHealth / 25)
            End While
            Console.ForegroundColor = ConsoleColor.DarkGray
            While HBar <= MaxHealth
                Console.Write("|")
                HBar += (MaxHealth / 25)
            End While
            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.Blue
            Console.Write("Mana:   ")
            Console.Write(mana)
            If MaxMana < 10 Then
                Console.Write("  /  ")
            End If
            If mana >= 10 Then
                Console.Write(" / ")
            End If
            If MaxMana >= 10 And mana < 10 Then
                Console.Write("  / ")
            End If
            Console.Write(MaxMana)
            Console.Write(" ")
            MBar = 1
            While MBar <= mana
                Console.Write("|")
                MBar += 1
            End While
            Console.ForegroundColor = ConsoleColor.DarkGray
            While MBar <= MaxMana
                Console.Write("|")
                MBar += 1
            End While
            Console.WriteLine()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            If charge = 0 Then
                MainPanel()
            Else
                If Player1.Target = 1 Then
                    TargetName = Monster1.Name
                End If
                If Player1.Target = 2 Then
                    TargetName = monster2.Name
                End If
                If Player1.Target = 3 Then
                    TargetName = monster3.Name
                End If
                If Player1.Target = 4 Then
                    TargetName = monster4.Name
                End If
                ref.ChargeAttack(power)
            End If
        End Sub
    End Class
    Class LevelSystem
        Public level As Integer = 1
        Public PlayerExp As Integer
        Sub LevelUp(ByVal z As Integer)
            Dim y As Integer
            If z = 1 Then
                EarnExp()
            End If
            If PlayerExp >= 100 Then
                PlayerExp = PlayerExp - 100
                y += 1
                level += 1
                If Player1.HeroClass = "Warrior" Then
                    Player1.MaxHealth = Player1.MaxHealth + 3
                    Player1.CriticalChance = Player1.CriticalChance + 0.3
                    If y Mod 3 = 0 Then
                        Player1.MaxMana = Player1.MaxMana + 1
                    End If
                    Player1.power = Player1.power + 2
                    If y Mod 2 = 0 Then
                        Player1.defence = Player1.defence + 1
                    End If
                End If
                If Player1.HeroClass = "Mage" Then
                    Player1.MaxHealth = Player1.MaxHealth + 1
                    Player1.CriticalChance = Player1.CriticalChance + 0.5
                    Player1.MaxMana = Player1.MaxMana + 1
                    Player1.EleMagic = Player1.EleMagic + 2
                    If y Mod 5 = 0 Then
                        Player1.defence = Player1.defence + 1
                    End If
                    Player1.HolyMagic = Player1.HolyMagic + 1
                End If
                If Player1.HeroClass = "Priest" Then
                    Player1.MaxHealth = Player1.MaxHealth + 2
                    Player1.CriticalChance = Player1.CriticalChance + 0.2
                    Player1.MaxMana = Player1.MaxMana + 1
                    If y Mod 3 = 0 Then
                        Player1.EleMagic = Player1.EleMagic + 1
                    End If
                End If
                If Player1.HeroClass = "Ranger" Then
                    Player1.MaxHealth = Player1.MaxHealth + 3
                    Player1.CriticalChance = Player1.CriticalChance + 0.4
                    If y Mod 3 = 0 Then
                        Player1.MaxMana = Player1.MaxMana + 1
                    End If
                    Player1.power = Player1.power + 1
                End If
                If Player1.HeroClass = "Boss" Then
                    Console.WriteLine("Bosses don't level up! Exp returning to 0")
                    level -= 1
                    PlayerExp = 0
                End If
                If z = 1 And Not Player1.HeroClass = "Boss" Then
                    Console.Write(Player1.name)
                    Console.WriteLine(" Leveled Up!")
                    Console.Write("Level: ")
                    Console.WriteLine(level)
                    Console.Write("Max Health: ")
                    Console.WriteLine(Player1.MaxHealth)
                    Console.Write("Max Mana: ")
                    Console.WriteLine(Player1.MaxMana)
                    Console.Write("Attack: ")
                    Console.WriteLine(Player1.power)
                    Console.Write("H. Magic: ")
                    Console.WriteLine(Player1.HolyMagic)
                    Console.Write("E. Magic: ")
                    Console.WriteLine(Player1.EleMagic)
                    Console.Write("Defense: ")
                    Console.WriteLine(Player1.defence)
                    Console.Write("Crit Chance: ")
                    Console.WriteLine(Player1.CriticalChance)
                End If
            End If
            If PlayerExp >= 100 Then
                LevelUp(z)
            End If
        End Sub
        Sub EarnExp()
            Dim a, b, c, d As Integer
            a = Monster1.exp - (level - 1)
            b = monster2.exp - (level - 1)
            c = monster3.exp - (level - 1)
            d = monster4.exp - (level - 1)
            If a < 0 Then
                a = 0
            End If
            If b < 0 Then
                b = 0
            End If
            If c < 0 Then
                c = 0
            End If
            If d < 0 Then
                d = 0
            End If
            PlayerExp = PlayerExp + a + b + c + d
            Console.Write("Experience earned: ")
            If a + b + c + d > 0 Then
                Console.WriteLine(a + b + c + d)
            Else
                PlayerExp = PlayerExp + 1
                Console.WriteLine(1)
            End If
            Console.Write("Experience Total:  ")
            Console.WriteLine(PlayerExp)
        End Sub
    End Class
    Class StoryMode
        Dim nice As Integer
        Dim area As Integer
#Region "Story1"
        Public Sub scenario1()
            Console.Clear()
            File.Load()
            If Progress = 0 Then
                Prologue()
            End If
            If Progress = 1 Then
                Act1Part1()
                Act1Part2()
                Act1Part3()
            End If
            If Progress = 2 Then
                act1part4()
            End If
            credits()
        End Sub
        Public Sub Prologue()
            Console.WriteLine("Scenario 1 Prologue: Why me? Why now?")
            Console.Write(" In the year 2192, the human race discovered the ability to harness a potent    energy: Magic. ")
            Console.Write("The endless power that encompassed the whole of the universe.    Magic is the only thing ")
            Console.Write("powerful enough to stay the ultimate powers of chaos,   and Magic is the only thing that can maintain order in the universe. It was     discovered that harnessing the ")
            Console.Write("powers of the ancient magical arts was more      effective then the greatest military technology ")
            Console.Write("of that age. At first, only few carefully chosen magi were able to harness this energy. ")
            Console.WriteLine("As the understanding of magic, and it's use spread, so did human's lust for it's power. ")
            Console.ReadKey()
            Console.Write(" In the year 2634, a mad wizard used the power of madness to summon a portal to the void in the center ")
            Console.Write("of the planet his lab was on, Vabor. This planet also    functioned as the imperial planet of a massive republic ")
            Console.Write("that sprawled across thespace in that sector. When the portal was created, the mad wizard was ")
            Console.Write("consumed  by the chaos that emerged. His soul was forever bound to guard the entrance to  the void ")
            Console.WriteLine(" The overworld of Vabor was over run by the demonic creatures that cameforth. ")
            Console.ReadKey()
            Console.Write("Millions of people were slain, and still millions more were evacuated. Luckily, even in the Human's ")
            Console.WriteLine("haste to embrace magic for military purposes, they did not  abandon the use of modern space transports. ")
            Console.ReadKey()
            Console.Write(" The creatures of the void are somewhat contained on that planet, but their     influence slowly creeps across to other planets. ")
            Console.WriteLine("The leaders of the sector have recently decided to attempt to retake the once glorious ruins of their beloved  homeworld. ")
            Console.ReadKey()
            Console.WriteLine()
            Console.Write("You are a soldier chosen to take part in creating a foothold in the ruins of theclosest city to the chasm ")
            Console.WriteLine("wrought by the void. The chasm is the only thing      keeping foot soldiers from reaching the capital city.")
            Console.ReadKey()
            Progress = 1
            File.Save()
        End Sub
        Public Sub Act1Part1()
            Console.WriteLine("Scenario 1 Act 1: The State of Things")
            Console.ReadKey()
            Console.WriteLine("You are aboard the transport to Vabor, along with several other soldiers and a  General.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Alright soldiers, we've been chosen to take part in a very        important mission.")
            Console.ReadKey()
            Console.WriteLine("General Cade: The task before us is great, Yet we must do it in the name of     reclaiming Vabor.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Our job is to land in the center of the city of Yaamata and set upa foothold.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Said foothold will be used by countless men after us in order to  begin the campaign across the chasm towards the capital.")
            Console.ReadKey()
            Console.WriteLine("General Cade: When we land, we will clear the vermin out immediately and begin  fortifying a large building nearby for use.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Any Questions? Good I thought not. You'd all better get some rest.Combat will begin as soon as we hit the ground.")
            Console.ReadKey()
            Console.WriteLine("The General's voice drops as he turns around,")
            Console.ReadKey()
            Console.WriteLine("If not before...")
            Console.ReadKey()
            Console.Clear()
        End Sub
        Public Sub Act1Part2()
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine("BOOOOOOOOOOOOOOOM!")
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            Console.WriteLine("An Earth shattering roar jars " & Player1.name & " from their sleep!")
            Console.ReadKey()
            Console.WriteLine("As General Cade had predicted, they were being shot at already!")
            Console.ReadKey()
            Console.WriteLine("You hop up out of your bunk and grab your equipment pack.")
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine("BOOOOOOOOOOOOOOOOM!")
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            Console.WriteLine("The transport rocks violently and almost knocks you to the ground.")
            Console.ReadKey()
            Console.WriteLine("An alarm starts blaring above your head, everyone is rushing around!")
            Console.ReadKey()        
            Console.WriteLine("The General's voice rings out from the Intercom system.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Attention, Everyone stay calm and proceed to the evacuation deck")
            Console.ReadKey()
            Console.WriteLine("You and the rest of your regiment sprint over to the area designated, Where     General Cade is waiting.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Everyone grab a gravity pack and jump when I say go. 1, 2...")
            Console.ReadKey()
            Console.WriteLine("The terrified soldier in front of you sprints off the deck.")
            Console.ReadKey()
            Console.WriteLine("A blood-curdling screech rends the air as a giant, black, winged beast zooms    past and grabs the unfortunate soldier in it's razer talons.")
            Console.ReadKey()
            Console.WriteLine("A gasp of shock emanates from someone a little ways behind you as the beast     drops the screaming man, gravity packless, with no chance of survival")
            Console.ReadKey()
            Console.WriteLine("General Cade: *Sighs* Already...")
            Console.ReadKey()
            Console.WriteLine("The beast turns around and zooms towards back towards the ship for another       victim.")
            Console.ReadKey()
            Console.WriteLine("It appears to be flying directly at " & Player1.name & "!")
            Console.ReadKey()
            Console.WriteLine("But this time, you're ready!")
            Console.ReadKey()
            Console.Clear()
            Combat(1, 2)
            Console.Clear()
        End Sub
        Public Sub Act1Part3()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            Console.WriteLine("The black eagle screeches out in pain as its essence returns to it's pure, dark form and floats away.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Not bad soldier, You've got some potential after all. What's your name?")
            Console.WriteLine(Player1.name & ": Private " & Player1.name & ", sir.")
            Console.ReadKey()
            Console.WriteLine("General Cade: Well, good job private. Now enough congratulations, we'll see     plenty more of those things on the ground.")
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine("BOOOOOOOOOOOM!")
            Console.ReadKey()
            Console.ForegroundColor = ConsoleColor.DarkBlue
            Console.WriteLine("The engines stop humming and the ship begins to pick up a speed and go into a   dive!")
            Console.ReadKey()
            Console.WriteLine("General Cade: EVERYBODY JUMP!")
            Console.ReadKey()
            Console.WriteLine("All the soldiers start running towards the edge of the evacuation deck and jump off the edge!")
            Console.ReadKey()
            Console.WriteLine("All manners of flying beasts screech through the air, knocking down soldiers.")
            Console.ReadKey()
            Console.WriteLine("The ground is spiraling beneath you as you free fall to the surface of the      planet.")
            Console.ReadKey()
            Console.WriteLine("The remaining soldiers' gravity packs kick in and they land safely on the ground")
            Console.WriteLine("Only to be greeted by more terrifying land bound beasts.")
            Console.ReadKey()
            Console.Clear()
            Progress = 2
            File.Save()
        End Sub
        Public Sub Act1Part4()

        End Sub
#End Region
        Sub credits()
            Dim count As Integer = 0
            Console.Clear()
            While count < 7
                If count < 1 Then
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.CursorTop)
                    Console.WriteLine("Thanks for playing!")
                End If
                If count < 2 Then
                    Console.WriteLine()
                End If
                If count < 3 Then
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.CursorTop)
                    Console.WriteLine(" Credits")
                End If
                If count < 4 Then
                    Console.WriteLine()
                End If
                If count < 5 Then
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.CursorTop)
                    Console.WriteLine("Developer")
                End If
                If count < 6 Then
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.CursorTop)
                    Console.WriteLine("Trey  Prior")
                End If
                Threading.Thread.SpinWait(500000000)
                count += 1
                Console.Clear()
                Console.SetCursorPosition(0, 0)
            End While
        End Sub
    End Class
    Class StatusSetting
        Sub SetPlayer1()
            Player1.dead = False
            Player1.health = Player1.MaxHealth
            Player1.mana = Player1.MaxMana
            Player1.charge = False
        End Sub
        Sub SetMonster1()
            Monster1.dead = False
            Monster1.Health = Monster1.MaxHealth
            Monster1.Mana = Monster1.MaxMana
            Monster1.charge = False
        End Sub
        Sub SetMonster2()
            monster2.dead = 0
            monster2.Health = monster2.MaxHealth
            monster2.Mana = monster2.MaxMana
            monster2.charge = 0
        End Sub
        Sub SetMonster3()
            monster3.dead = 0
            monster3.Health = monster3.MaxHealth
            monster3.Mana = monster3.MaxMana
            monster3.dead = 0
        End Sub
        Sub SetMonster4()
            monster4.dead = 0
            monster4.Health = monster4.MaxHealth
            monster4.Mana = monster4.MaxMana
            monster4.charge = 0
        End Sub
    End Class
    Class SaveLoad
        Dim filepath As String
        Sub Save()
            Dim l As Integer
            Dim p As Integer = 0
            Console.WriteLine()
            console.writeline("Would You like to save? 1. Yes, 2. No")
            Try
                l = console.read()
            Catch ex As exception
                errormessage()
                Exit Sub
            End Try
            If l <> 1 Or l <> 2 Then
                errormessage()
                Exit Sub
            ElseIf l = 2 Then
                Exit Sub
            End If
            Console.WriteLine("Please select a name for your save file. Note: Choosing an already taken name will result in overwriting the file.")
            filepath = Console.ReadLine()
            If IO.File.Exists("saves1\" & filepath & "Progress") = True Or IO.File.Exists("saves1\" & filepath & "Level") = True Or IO.File.Exists("saves1/" & filepath & "Class") Then
                While p = 0
                    p = 1
                    Console.WriteLine("Are you sure you want to overwrite this file? 1. Yes, 2. No ")
                    Try
                        l = Console.ReadLine
                    Catch ex As Exception
                        ErrorMessage()
                        p = 0
                    End Try
                    If l <> 1 Or l <> 2 Then
                        errormessage()
                        p = 0
                    End If
                    If l = 2 Then
                        Exit Sub
                    End If
                End While
                If IO.Directory.Exists("saves1") = False Then
                    IO.Directory.CreateDirectory("saves1")
                End If
                Try
                    IO.File.WriteAllText("saves1\" & filepath & "Progress", Progress)
                    IO.File.WriteAllText("saves1\" & filepath & "Level", (Level.level * 100 + Level.PlayerExp))
                    IO.File.WriteAllText("saves1\" & filepath & "Class", Player1.ClassChoice)
                    IO.File.WriteAllText("saves1\" & filepath & "Name", Player1.name)
                Catch ex As Exception
                    Console.WriteLine("The file could not be written to, Either you have deleted/moved the folder labeled saves, or you have moved the game. Please remedy this in order to save and load again")
                    save()
                    Exit Sub
                End Try
            End If
            Console.WriteLine("Save Successful")
            Console.ReadKey()
            Console.Clear()
        End Sub
        Sub Load()
            Dim r As Integer = 0
            While r = 0
                Console.WriteLine("What would you like to do? 1. Load Game 2. New Game")
                Try
                    r = Console.ReadLine
                Catch ex As Exception
                    ErrorMessage()
                    r = 0
                End Try
                If r = 0 Then
                    ErrorMessage()
                End If
                If r = 1 Then
                    r = 1
                    Console.WriteLine("Please input the Filename that you wish to load")
                    Try
                        filepath = Console.ReadLine
                    Catch ex As Exception
                        ErrorMessage()
                        r = 0
                    End Try
                    If IO.File.Exists("saves1\" & filepath & "Progress") = True And IO.File.Exists("saves1\" & filepath & "Level") = True Then
                        Level.PlayerExp = IO.File.ReadAllText("saves1\" & filepath & "Level")
                        Progress = IO.File.ReadAllText("saves1\" & filepath & "Progress")
                        Player1.name = IO.File.ReadAllText("saves1\" & filepath & "Name")
                        Player1.ClassChoice = IO.File.ReadAllText("saves1\" & filepath & "Class")
                        Player1.SetStats(Player1.ClassChoice)
                        Level.LevelUp(0)
                        Console.WriteLine("Load Successful")
                        Console.ReadKey()
                    Else
                        Console.WriteLine("File does not exist; Please ensure you input the correct file name. If you did, one or more of the files may have been deleted, moved or corrupted.")
                        r = 0
                    End If
                Else
                    ChooseName()
                    ChooseClass()
                    Player1.SetStats(Player1.ClassChoice)
                End If
            End While
            Console.Clear()
        End Sub
    End Class
#End Region
#Region "Subs"
    Function CriticalHit(ByVal x As Integer)
        Dim y As Integer
        y = rnd.Next(1, 101)
        If user = Player1.name Then
            If y <= Player1.CriticalChance Then
                Console.WriteLine("Critical Hit!")
                x = x * 1.5
            End If
        End If
        If user = Monster1.Name Then
            If y <= Monster1.CriticalChance Then
                Console.WriteLine("Critical Hit!")
                x = x * 1.5
            End If
            If user = monster2.Name Then
                If y <= monster2.CriticalChance Then
                    Console.WriteLine("Critical Hit!")
                    x = x * 1.5
                End If
                If user = monster3.Name Then
                    If y <= monster3.CriticalChance Then
                        Console.WriteLine("Critical Hit!")
                        x = x * 1.5
                    End If
                    If user = monster4.Name Then
                        If y <= monster3.CriticalChance Then
                            Console.WriteLine("Critical Hit!")
                            x = x * 1.5
                        End If
                    End If
                End If
            End If
        End If
        Return x
    End Function
    Sub ErrorMessage()
        Console.WriteLine("Invalid Input: Try Again")
    End Sub
    Sub ChooseMode()
        Console.WriteLine("Please select a mode. 0. Free Battle, 1. Story Mode 1, or 2. Credits")
        Try
            mode = Console.ReadLine
        Catch ex As Exception
            ErrorMessage()
            ChooseMode()
            Exit Sub
        End Try
        Select Case (mode)
            Case 0
                FreeBattle()
            Case 1
                Story.scenario1()
            Case 2
                Story.credits()
                ChooseMode()
            Case Else
                ErrorMessage()
                ChooseMode()
                Exit Sub
        End Select
    End Sub
    Sub SetEnemy(Optional ByVal l As Integer = 0)
        If mode = 0 Then
            ChooseEnemy()
        Else
            n = l
        End If
    End Sub
    Sub ChooseEnemy()
        Dim y As Integer
        Dim b As Integer = 1
        n = -1
        While b = 1
            Console.WriteLine("Choose a catagory")
            Console.WriteLine("0. Training Dummy")
            Console.WriteLine("1. Easy")
            Console.WriteLine("2. Medium")
            Console.WriteLine("3. Hard")
            Console.WriteLine("4. Boss")
            Try
                y = Console.ReadLine
            Catch ex As Exception
                ErrorMessage()
                b = 1
            End Try
            If y < 0 Or y > 5 Then
                ErrorMessage()
                b = 1
            Else
                b = 0
            End If
        End While
        While n = -1
            If b <> 1 Then
                Select Case (y)
                    Case 0
                        n = 0
                    Case 1
                        Console.WriteLine("Choose an Enemy")
                        Console.WriteLine("1. Rabid Chicken")
                        Console.WriteLine("2. Black Eagle")
                        Try
                            n = Console.ReadLine
                        Catch ex As Exception
                            n = -1
                        End Try
                        If n < 1 Or n > 30 Then
                            ErrorMessage()
                            n = -1
                        End If
                    Case 2
                        Console.WriteLine("Choose an Enemy")
                        Console.WriteLine("31. Ogre")
                        Try
                            n = Console.ReadLine
                        Catch ex As Exception
                            n = -1
                        End Try
                        If n > 60 Or n < 31 Then
                            ErrorMessage()
                            n = -1
                        End If
                    Case 3
                        Console.WriteLine("Choose an Enemy")
                        Console.WriteLine("61. Giant")
                        Try
                            n = Console.ReadLine
                        Catch ex As Exception
                            n = -1
                        End Try
                        If n < 60 Or n > 90 Then
                            ErrorMessage()
                            n = -1
                        End If
                    Case 4
                        Console.WriteLine("Choose an Enemy")
                        Try
                            n = Console.ReadLine
                        Catch ex As Exception
                            ErrorMessage()
                            n = -1
                        End Try
                        If n < 91 Then
                            ErrorMessage()
                            n = -1
                        End If
                End Select
            End If
        End While
        Console.Clear()
    End Sub
    Sub ChooseName()
        Try
            Console.WriteLine("Input the Hero's name")
            Player1.name = Console.ReadLine
        Catch ex As Exception
            Console.WriteLine("Invalid Input: Try Again")
            ChooseName()
        End Try
    End Sub
    Sub ChooseClass()
        Level.PlayerExp = 0
        Console.Write("Please select ")
        Console.Write(Player1.name)
        Console.WriteLine("'s class. 1. Warrior, 2. Mage, 3. Priest, 4. Ranger")
        Try
            Player1.ClassChoice = Console.ReadLine
        Catch ex As Exception
            ErrorMessage()
            ChooseClass()
            Exit Sub
        End Try
        If (Player1.ClassChoice > 4 Or Player1.ClassChoice < 1) And Not Player1.ClassChoice = 100 Then
            ErrorMessage()
            Console.WriteLine()
            ChooseClass()
        End If
        Console.Clear()
    End Sub
    Sub Combat(ByVal x As Integer, Optional ByVal l As Integer = 0, Optional ByVal l2 As Integer = 0, Optional ByVal l3 As Integer = 0, Optional ByVal l4 As Integer = 0)
        'Set Opponents
        z = 1
        monst.NumberOfEnemies = x
        SetEnemy(l)
        Monster1.Type = n
        Monster1.SetStats(Monster1.Type)
        StatusSetter.SetMonster1()
        StatusSetter.SetPlayer1()
        If monst.NumberOfEnemies > 1 Then
            SetEnemy(l2)
            monster2.Type = n
            monster2.SetStats(monster2.Type)
            StatusSetter.SetMonster2()
        End If
        If monst.NumberOfEnemies > 2 Then
            SetEnemy(l3)
            monster3.Type = n
            monster3.SetStats(monster3.Type)
            StatusSetter.SetMonster3()
        End If
        If monst.NumberOfEnemies > 3 Then
            SetEnemy(l4)
            monster4.Type = n
            monster4.SetStats(monster4.Type)
            StatusSetter.SetMonster4()
        End If
        Select Case (x)
            Case 1
                If Monster1.Name(1) = "A" Or Monster1.Name(1) = "E" Or Monster1.Name(1) = "I" Or Monster1.Name(1) = "O" Or Monster1.Name(1) = "U" Then
                    Console.Write("An ")
                Else
                    Console.Write("A ")
                End If
            Case 2
                Console.Write("Two ")
            Case 3
                Console.Write("Three ")
            Case 4
                Console.Write("Four ")
        End Select
        If x > 1 Then
            Console.Write("Foes")
        Else
            Console.Write(Monster1.Name)
        End If
        If x = 1 Then
            Console.Write(" Appears in ")
        Else
            Console.Write(" Appear in ")
        End If
        Console.Write(Player1.name)
        Console.WriteLine("'s path!")
        Console.WriteLine(Monster1.Name)
        If monster2.dead = False Then
            Console.WriteLine(monster2.Name)
        End If
        If monster3.dead = False Then
            Console.WriteLine(monster3.Name)
        End If
        If monster4.dead = False Then
            Console.WriteLine(monster4.Name)
        End If
        If music = 1 Then
            background.Stop()
            background.SoundLocation = ("resources\music\Final Fantasy IV DS Music - Four Emperors (Dreadful Fight).wav")
            background.Load()
            background.PlayLooping()
        End If
        'Reset necessary variables/stats
        Dim Poisened As Boolean = False
        Dim Sleeping As Boolean = False
        Dim Frozen As Boolean = False
        Dim ArmorBroken As Boolean = False
        Dim turn As Integer = 5
        'Begin Battle Loop
        Dim LoopVar As Integer = 1
        While LoopVar = 1
            'Take Turns
            Select Case (turn)
                Case 1
                    If Monster1.dead = False Then
                        user = Monster1.Name
                        Monster1.Turn()
                    End If
                Case 2
                    If monster2.dead = False Then
                        user = monster2.Name
                        monster2.Turn()
                    End If
                Case 3
                    If monster3.dead = False Then
                        user = monster3.Name
                        monster3.Turn()
                    End If
                Case 4
                    If monster4.dead = False Then
                        user = monster4.Name
                        monster4.Turn()
                    End If
                    Console.ReadKey()
                    Console.Clear()
                Case 5
                    user = Player1.name
                    Player1.Turn()
            End Select
            Turn += 1
            If Turn = 6 Then
                Turn = 1
            End If
            ' Set dead entities to dead
            If player1.health < 1 Then
                Player1.dead = True
            End If
            If Monster1.Health < 1 And Monster1.dead = False Then
                Console.Write("The ")
                Console.Write(Monster1.Name)
                Console.WriteLine(" crumples to the ground and evaporates")
                Monster1.dead = True
            End If
            If monster2.Health < 1 And monster2.dead = False Then
                Console.Write("The ")
                Console.Write(monster2.Name)
                Console.WriteLine(" crumples to the ground and evaporates")
                monster2.dead = True
            End If
            If monster3.Health < 1 And monster3.dead = False Then
                Console.Write("The ")
                Console.Write(monster3.Name)
                Console.WriteLine(" crumples to the ground and evaporates")
                monster3.dead = True
            End If
            If monster4.Health < 1 And monster4.dead = False Then
                Console.Write("The ")
                Console.Write(monster4.Name)
                Console.WriteLine(" crumples to the ground and evaporates")
                monster4.dead = True
            End If
            ' Check Life Status
            If Player1.dead = True Then
                Gameover()
                LoopVar = 0
            End If
            If Monster1.dead = True And monster2.dead = True And monster3.dead = True And monster4.dead = True Then
                'Give Reward
                Victory()
                LoopVar = 0
            End If
            'End Battle Loop
        End While
        'Cleanup
        Monster1.dead = True
        monster2.dead = True
        monster3.dead = True
        monster4.dead = True
    End Sub
    Sub Gameover()
        Console.WriteLine()
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write(Player1.name)
        Console.WriteLine(" falls to the ground, knocked unconcious!")
        Console.WriteLine("Game Over!")
        If Monster1.dead = 0 Then
            Console.Write("The ")
            Console.Write(Monster1.Name)
            Console.Write(" had: ")
            Console.Write(Monster1.Health)
            Console.Write(" health remaining")
            Console.WriteLine()
        End If
        If monster2.dead = 0 Then
            Console.Write("The ")
            Console.Write(monster2.Name)
            Console.Write(" had: ")
            Console.Write(monster2.Health)
            Console.Write(" health remaining")
            Console.WriteLine()
        End If
        If monster3.dead = 0 Then
            Console.Write("The ")
            Console.Write(monster3.Name)
            Console.Write(" had: ")
            Console.Write(monster3.Health)
            Console.Write(" health remaining")
            Console.WriteLine()
        End If
        If monster4.dead = 0 Then
            Console.Write("The ")
            Console.Write(monster4.Name)
            Console.Write(" had: ")
            Console.Write(monster4.Health)
            Console.Write(" health remaining")
            Console.WriteLine()
        End If
    End Sub
    Sub Victory()
        Console.ForegroundColor = ConsoleColor.DarkGreen
        Console.Write("The ")
        If monst.NumberOfEnemies = 1 Then
            Console.WriteLine(Monster1.Name & " was defeated!")
        Else
            Console.WriteLine("Enemies were defeated!")
        End If
        'Drops and Level up
        Level.LevelUp(1)
    End Sub
    Sub FreeBattle()
        Dim y As Integer
        Dim x As Integer = 0
        Console.Clear()
        ChooseName()
        Console.Clear()
        ChooseClass()
        Player1.SetStats(Player1.ClassChoice)
        While PlayAgain = 1
            t = 0
            Console.WriteLine("How many enemies would you like to face?")
            Try
                y = Console.ReadLine
            Catch ex As Exception
                ErrorMessage()
                y = rnd.Next(1, 5)
            End Try
            If y > 4 Or y < 0 Then
                ErrorMessage()
            End If
            Combat(y)
            While x = 0
                Try
                    Console.WriteLine("Would you like to go again? 1 for yes or 2 for no")
                    x = Console.ReadLine
                Catch ex As Exception
                    ErrorMessage()
                    x = 0
                End Try
                If x = 2 Then
                    Console.ForegroundColor = ConsoleColor.DarkBlue
                    Console.WriteLine("Ok, bye then")
                    Console.WriteLine("Press any key to exit")
                    Console.ReadKey()
                    PlayAgain = 0
                ElseIf x = 1 Then
                    Console.ForegroundColor = ConsoleColor.DarkBlue
                    Console.WriteLine("Alright, here we go again")
                    Console.ReadKey()
                    Console.Clear()
                Else
                    ErrorMessage()
                    x = 0
                End If
            End While
        End While
    End Sub
    Sub SetStatus()
        StatusSetter.SetPlayer1()
        StatusSetter.SetMonster1()
        If monst.NumberOfEnemies > 1 Then
            StatusSetter.SetMonster2()
        Else
            monster2.dead = 1
        End If
        If monst.NumberOfEnemies > 2 Then
            StatusSetter.SetMonster3()
        Else
            monster3.dead = 1
        End If
        If monst.NumberOfEnemies > 3 Then
            StatusSetter.SetMonster4()
        Else
            monster4.dead = 1
        End If
    End Sub
    Sub IsMusicenabled()
        Console.WriteLine("Would you like to play with music enabled? 1. Yes 2. No")
        Try
            music = Console.ReadLine()
        Catch ex As Exception
            ErrorMessage()
            IsMusicenabled()
            Exit Sub
        End Try
        If music <> 1 And music <> 2 Then
            ErrorMessage()
            IsMusicenabled()
            Exit Sub
        End If
        Console.Clear()
    End Sub
    Sub closeprogram()
        background.Stop()
        background.Dispose()
    End Sub
    Sub timer(ByVal x As Integer)
        Dim y As Integer = 0
        x = x * 75000
        While y < x
            y += 1
        End While
    End Sub
#End Region
    Sub main()
        Console.Title = "Heart of the Void"
        Console.BackgroundColor = ConsoleColor.Gray
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.DarkBlue
        IsMusicenabled()
        If music = 1 Then
            background.SoundLocation = ("")
            'background.PlayLooping()
        End If
        Console.ForegroundColor = ConsoleColor.DarkMagenta
        Console.CursorVisible = False
        Console.WriteLine("  _   _                 _            __   _   _            _   _       _     _ ")
        Console.WriteLine(" | | | |               | |          / _| | | | |          | | | |     (_)   | |")
        Console.WriteLine(" | |_| | ___  __ _ _ __| |_    ___ | |_  | |_| |__   ___  | | | | ___  _  __| |")
        Console.WriteLine(" |  _  |/ _ \/ _` | '__| __|  / _ \|  _| | __| '_ \ / _ \ | | | |/ _ \| |/ _` |")
        Console.WriteLine(" | | | |  __/ (_| | |  | |_  | (_) | |   | |_| | | |  __/ \ \_/ / (_) | | (_| |")
        Console.WriteLine(" |_| |_|\___|\__,_|_|   \__|  \___/|_|    \__|_| |_|\___|  \___/ \___/|_|\__,_|")
        Console.WriteLine("             _   _               _               __        _____ ")
        Console.WriteLine("            | | | |             (_)             /  |      / __  \")
        Console.WriteLine("            | | | | ___ _ __ ___ _  ___  _ __   `| |      `' / / ")
        Console.WriteLine("            | | | |/ _ \ '__/ __| |/ _ \| '_ \   | |        / /  ")
        Console.WriteLine("            \ \_/ /  __/ |  \__ \ | (_) | | | | _| |_  _   / /___")
        Console.WriteLine("             \___/ \___|_|  |___/_|\___/|_| |_| \___/ (_) \_____/")
        Console.ReadKey(True)
        Console.ForegroundColor = ConsoleColor.DarkBlue
        Console.Clear()
        Console.CursorVisible = True
        ChooseMode()
        Console.ReadKey(True)
    End Sub
End Module
