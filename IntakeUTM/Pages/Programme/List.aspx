<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="IntakeUTM.Pages.Programme.Programmes" %>
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
                        <td>
                            <asp:HyperLink ID="ProgrammeLink" runat="server" NavigateUrl='<%# "~/Pages/Programme/View.aspx?Id=" + ((Programme)Container.DataItem).Id %>'>
                                <%# ((Programme)Container.DataItem).Code %>
                            </asp:HyperLink>
                        </td>
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
