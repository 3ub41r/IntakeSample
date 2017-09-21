<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="IntakeUTM.Samples.Template" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h1>Template</h1>
    <h3>
        <asp:Literal ID="ProgrammeLiteral" runat="server"></asp:Literal>
    </h3>
    <asp:TextBox ID="OfferLetterText" CssClass="wysiwyg" TextMode="MultiLine" runat="server"></asp:TextBox>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
