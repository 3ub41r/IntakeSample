<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="~/Samples/View.aspx.cs" Inherits="IntakeUTM.Pages.Programme.ProgrammeDetails" %>
<%@ Import Namespace="IntakeUTM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>
        <asp:Literal ID="ProgrammeName" runat="server"></asp:Literal>
    </h1>
    
    <div class="panel panel-default">
        <div class="panel-heading">
            <h3 class="panel-title">Templates</h3>
        </div>
        <div class="panel-body">
            <asp:HyperLink ID="AddButton" CssClass="btn btn-primary" ToolTip="Add Template" runat="server">
                Add
            </asp:HyperLink>
            <asp:HyperLink ID="BackButton" NavigateUrl="~/Pages/Programme/List.aspx" CssClass="btn btn-link" runat="server">Back</asp:HyperLink>
        </div>
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
                        <td>
                            <asp:HyperLink ID="EditLink" NavigateUrl='<%# "~/Pages/PagesTemplate/View.aspx?Id=" + ((PagesTemplate) Container.DataItem).Id %>' runat="server">
                                <%# ((PagesTemplate) Container.DataItem).Name %>
                            </asp:HyperLink>
                        </td>
                        <td><%# ((PagesTemplate) Container.DataItem).FileLocation%></td>
                        <td><%# ((PagesTemplate) Container.DataItem).SortOrder %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
