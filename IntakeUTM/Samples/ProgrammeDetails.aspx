<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProgrammeDetails.aspx.cs" Inherits="IntakeUTM.Samples.ProgrammeDetails" %>
<%@ Import Namespace="IntakeUTM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>
        <asp:Literal ID="ProgrammeName" runat="server"></asp:Literal>
    </h1>
    <h3>Templates</h3>
    <asp:HyperLink ID="AddButton" CssClass="btn btn-primary" ToolTip="Add Template" runat="server">
        Add
    </asp:HyperLink>
    <asp:HyperLink ID="BackButton" NavigateUrl="Programmes.aspx" CssClass="btn btn-link" runat="server">Back</asp:HyperLink>

    <table class="table">
        <thead>
            <tr>
                <th>Name</th>
                <th>Location</th>
                <th>Sort Order</th>
            </tr>
        </thead>
        <tbody>
            <asp:Repeater ID="DataRepeater" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# ((Template) Container.DataItem).Name %></td>
                        <td><%# ((Template) Container.DataItem).FileLocation%></td>
                        <td><%# ((Template) Container.DataItem).SortOrder %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
