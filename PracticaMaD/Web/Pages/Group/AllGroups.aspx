<%@ Page Title="" Language="C#" MasterPageFile="~/PracticaMaD.Master" AutoEventWireup="true" CodeBehind="AllGroups.aspx.cs" Inherits="Es.Udc.DotNet.PracticaMaD.Web.Pages.Group.AllGroups" 
    meta:resourcekey="Page" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder_MenuExplanation" runat="server">
    -
    <asp:Localize ID="Localize1" runat="server" meta:resourcekey="lclMenuExplanation" />
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder_BodyContent" runat="server">
    <form id="formGroups" method="POST" runat="server">
        <div class="field" > 
            <strong>
                <asp:Label ID="lblGroups" meta:resourcekey="lblGroups" runat="server"></asp:Label><br/>
            </strong>
            <asp:Label ID="lblNoGroups" meta:resourcekey="lblNoGroups" runat="server"></asp:Label><br/>

            <asp:GridView ID="gvGroups" runat="server" CssClass="products"
                AutoGenerateColumns="False" align="center" OnRowDataBound="gvGroups_RowDataBound" meta:resourcekey="gvGroupsResource1">
                <Columns>
                    <asp:BoundField DataField="groupName" HeaderText="<%$ Resources: groupName %>" meta:resourcekey="BoundFieldResource2" />
                    <asp:BoundField DataField="numberOfMembers" HeaderText="<%$ Resources: numberOfMembers %>" meta:resourcekey="BoundFieldResource3" />
                    <asp:BoundField DataField="numRecommendations"  HeaderText="<%$ Resources: numRecommendations %>" meta:resourcekey="BoundFieldResource4" />
                    <asp:TemplateField >
                        <ItemTemplate>
                            <asp:LinkButton ID="btnJoinGroup" runat="server" CommandArgument='<%# Eval("groupId") %>'  OnClick="btnJoinGroup_Click" meta:resourcekey="btnJoinGroup"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>  
        </div>
        <br />
            <!-- "Previous" and "Next" links. -->
        <div class="previousNextLinks">
            <span class="previousLink">
                <asp:HyperLink ID="lnkPrevious" runat="server"
                    Visible="False" meta:resourcekey="lnkPreviousResource1"></asp:HyperLink>
            </span><span class="nextLink">
                <asp:HyperLink ID="lnkNext" runat="server" Visible="False" meta:resourcekey="lnkNextResource1"></asp:HyperLink>
            </span>
        </div>
    </form>
    <br />
</asp:Content>