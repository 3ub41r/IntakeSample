<%@ Page Language="C#" AutoEventWireup="true" Title="Surat Tawaran" MasterPageFile="~/Main.Master" ValidateRequest="false" CodeBehind="SuratTawaran.aspx.cs" Inherits="IntakeUTM.Samples.SuratTawaran" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <h2 class="page-header">Surat Tawaran</h2>
        
    <div class="table-responsive">
        <table class="table">
            <thead>
            <tr>
                <th>Name</th>
                <th>Reference Number</th>
                <th>Email</th>
                <th>&nbsp;</th>
            </tr>
            </thead>
            <tbody>
            <asp:Repeater ID="ApplicationRepeater" runat="server" OnItemCommand="ApplicationRepeater_OnItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("Name") %></td>
                        <td><%# Eval("RefNumber") %></td>
                        <td><%# Eval("Email") %></td>
                        <td>
                            <asp:LinkButton ID="LetterButton" runat="server" CommandArgument='<%# Eval("Id") %>'>
                                Kemaskini Surat Tawaran
                            </asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            </tbody>
        </table>
    </div>
    <asp:HiddenField ID="HiddenId" runat="server" />
    <div class="form-group">
        <asp:TextBox ID="OfferLetterText" CssClass="wysiwyg" TextMode="MultiLine" runat="server"></asp:TextBox>
    </div>
    <div class="form-group">
        <asp:LinkButton ID="UpdateButton" OnClick="UpdateButton_OnClick" CssClass="btn btn-primary" runat="server">
            Kemaskini
        </asp:LinkButton>
    </div>
</asp:Content>

<asp:Content ID="FooterContent" ContentPlaceHolderID="FooterPlaceHolder" runat="server">
    <script src="../Scripts/summernote/summernote.min.js"></script>
    <script>
        $(function() {
            $('.wysiwyg').summernote();
        });
    </script>
</asp:Content>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderPlaceHolder" runat="server">
    <link rel="stylesheet" href="../Scripts/summernote/summernote.css">
</asp:Content>