<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Site.Master" CodeBehind="SuratTawaran.aspx.cs" Inherits="IntakeUTM.Samples.SuratTawaran" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Surat Tawaran</title>
    <style>
        .app-table {
            border-collapse: collapse;    
        }
        .app-table td, .app-table th {
            padding: 5px;
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Surat Tawaran</h1>
            
            <table class="app-table">
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Reference Number</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="ApplicationRepeater" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("Name") %></td>
                                <td><%# Eval("RefNumber") %></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </form>
</body>
</html>
