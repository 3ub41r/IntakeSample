﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Template.aspx.cs" Inherits="IntakeUTM.Samples.Template" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    
    <h1>Template</h1>
    <h3>
        <asp:Literal ID="ProgrammeLiteral" runat="server"></asp:Literal>
    </h3>
    <div class="form-group">
        <label>Name</label>
        <asp:TextBox ID="TemplateName" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <label>Language</label>
        <asp:DropDownList ID="Language" CssClass="form-control" runat="server">
            <asp:ListItem>English</asp:ListItem>
            <asp:ListItem>Malay</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Application Status</label>
        <asp:DropDownList ID="AppStatusListId" CssClass="form-control" runat="server">
        </asp:DropDownList>
    </div>
    <div class="form-group">
        <label>Sort Order</label>
        <asp:TextBox ID="SortOrder" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:TextBox ID="OfferLetterText" CssClass="wysiwyg" TextMode="MultiLine" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:LinkButton ID="SaveButton" CssClass="btn btn-primary" runat="server" OnClick="SaveButton_OnClick">Save</asp:LinkButton>
        <asp:HyperLink ID="BackLink" CssClass="btn btn-link" runat="server">Back</asp:HyperLink>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
</asp:Content>
