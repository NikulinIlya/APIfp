<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainWindow.aspx.cs" Inherits="WebGUI.MainWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>Окно ввода данных</h1>
        <h3>Введите строку вычислений</h3>
    </div>
    <div>
        <label>Ввод формулы: </label><input type="text" id="Formula" runat="server"/>
    </div>
    <div>
        <label>Ответ: </label><input type="text" id="Result" runat="server"/>
    </div>
    <div>
        <p>Примеры формул:</p>
        <p>.Value, .Then, .JoinValues, .Map, .All, .Any, .FlatMap, .Fold, .Recurse, .Zip, .Unzip, .If.Then.Else, .Every</p>
    </div>
    <div>
        <button type="submit">ОК</button>
    </div>
    </form>
</body>
</html>
