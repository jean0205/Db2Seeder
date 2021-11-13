Module ModHelps
    Public Function Checkdigitcalc(ByVal numb As Integer) As Integer
        Dim Result As Integer = 0
        Dim num1, num2, num3, num4, num5, num6, num7, num8, num9 As Integer
        Dim sum1a, sum2a, sum3a, sum4a, sum5a, sum1b, sum2b, sum3b, sum4b, sum5b As Integer
        Dim wrka, wrka2, sum1t, sum2t, sum3t, sum4t, sum5t As Integer
        Dim top, bottom, prechk, calc1, prechk1, prechk2, chkdig As Integer
        num1 = (numb \ 100000000)
        num2 = (numb \ 10000000) Mod 10
        num3 = (numb \ 1000000) Mod 10
        num4 = (numb \ 100000) Mod 10
        num5 = (numb \ 10000) Mod 10
        num6 = (numb \ 1000) Mod 10
        num7 = (numb \ 100) Mod 10
        num8 = (numb \ 10) Mod 10
        num9 = numb Mod 10

        'step 1

        sum1a = 0
        sum2a = num2
        sum3a = num4
        sum4a = num6
        sum5a = num8

        wrka = sum1a * 10000 + sum2a * 1000 + sum3a * 100 + sum4a * 10 + sum5a
        wrka2 = wrka * 2

        sum1t = (wrka2 \ 10000)
        sum2t = (wrka2 \ 1000) Mod 10
        sum3t = (wrka2 \ 100) Mod 10
        sum4t = (wrka2 \ 10) Mod 10
        sum5t = wrka2 Mod 10

        top = sum1t + sum2t + sum3t + sum4t + sum5t

        ' step 2
        sum1b = 0
        sum2b = num1
        sum3b = num3
        sum4b = num5
        sum5b = num7

        bottom = sum1b + sum2b + sum3b + sum4b + sum5b

        ' step 3
        prechk = top + bottom
        prechk1 = prechk \ 10
        calc1 = (prechk1 + 1) * 10

        prechk2 = calc1 - prechk

        If prechk2 > 9 Then
            chkdig = prechk2 - (prechk2 \ 10) * 10
        Else
            chkdig = prechk2
        End If
        Result = 100000000 * num1 + 10000000 * num2 + 1000000 * num3 + 100000 * num4 + 10000 * num5 + 1000 * num6 + 100 * num7 + 10 * num8 + chkdig
        Checkdigitcalc = Result
    End Function


End Module
