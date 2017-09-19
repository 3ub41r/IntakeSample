<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="../Site.Master" CodeBehind="SuratTawaran.aspx.cs" Inherits="IntakeUTM.Samples.SuratTawaran" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Surat Tawaran</h1>
            
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
        <asp:TextBox ID="OfferLetterText" TextMode="MultiLine" runat="server"></asp:TextBox>

    </div>
</asp:Content>