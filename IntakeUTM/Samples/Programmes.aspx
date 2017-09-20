<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Programmes.aspx.cs" Inherits="IntakeUTM.Samples.Programmes" %>
<%@ Import Namespace="IntakeUTM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h3>Programmes</h3>
    <table class="table">
        <thead>
            <tr>
                <th>Code</th>
                <th>Name</th>
                <th>Type</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="ProgrammesRepeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# ((Programme)Container.DataItem).Code %></td>
                        <td><%# ((Programme)Container.DataItem).Name %></td>
                        <td><%# ((Programme)Container.DataItem).ProgrammeType %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
