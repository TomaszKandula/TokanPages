import "../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "enzyme";
import { RenderSideMenu } from "./renderSideMenu";
import { Item } from "./Models";

describe("test render function 'RenderSideMenu'", () => {
    const noItems = render(
        <BrowserRouter>
            <RenderSideMenu isAnonymous={true} items={undefined}></RenderSideMenu>
        </BrowserRouter>
    );

    it("should return 'Cannot render content.' when called with items undefined.", () => {
        expect(noItems).toMatchSnapshot();
    });

    const emptyItems = render(
        <BrowserRouter>
            <RenderSideMenu isAnonymous={true} items={[]}></RenderSideMenu>
        </BrowserRouter>
    );

    it("should return 'Cannot render content.' when called with empty array of items.", () => {
        expect(emptyItems).toMatchSnapshot();
    });

    const items: Item[] = [
        {
            id: "c44bb2cd-ee75-470c-942e-5560e3589102",
            type: "item",
            value: "Home",
            link: "/",
            icon: "Home",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: false,
        },
        {
            id: "d253bd15-b1d4-4d91-b00c-9c6a3e43ca88",
            type: "divider",
            value: "middle",
        },
        {
            id: "5d762733-6724-4b4f-bc8c-7ede6baf77d5",
            type: "item",
            value: "Sign in",
            link: "/signin",
            icon: "Vpn_Key",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "bdd33e5c-f942-4b14-96c7-16523f693c1c",
            type: "item",
            value: "Sign up",
            link: "/signup",
            icon: "Person_Add",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "e8284fe7-5ced-46a5-b779-2fe516fe88c2",
            type: "itemspan",
            value: "Account",
            link: "/account",
            icon: "Person",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
            subitems: [
                {
                    id: "c4314ddd-4519-4951-92ff-160744b27fec",
                    type: "subitem",
                    value: "User articles",
                    link: "/userarticles",
                    icon: "Menu_Book",
                    enabled: false,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "f30d37d8-8c65-4610-a6e5-d9477445788d",
                    type: "subitem",
                    value: "User notes",
                    link: "/usernotes",
                    icon: "Notes",
                    enabled: false,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "dfc55ebf-7180-462d-9509-34ca7f87f5b9",
                    type: "subitem",
                    value: "User files",
                    link: "/userfiles",
                    icon: "File_Copy",
                    enabled: false,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "20f31599-5bb2-40d0-83b3-ab3aae4e15b2",
                    type: "subitem",
                    value: "User settings",
                    link: "/account",
                    icon: "Settings",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "d3ada751-012d-4c58-a4dd-a89c89928338",
                    type: "subitem",
                    value: "Sign out",
                    link: "/signout",
                    icon: "Lock",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
            ],
        },
        {
            id: "945b6c9c-f37f-4b03-bb6f-3256a559fafe",
            type: "divider",
            value: "middle",
        },
        {
            id: "a90bc6a8-237b-40c2-9865-68829e8df1a4",
            type: "item",
            value: "Showcase",
            link: "/showcase",
            icon: "Video_Library",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "d5303362-76fb-4638-a6ee-5077cac76826",
            type: "itemspan",
            value: "Github",
            link: "#",
            icon: "Github",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: false,
            subitems: [
                {
                    id: "34eecbfe-4b8d-4a96-b5dc-a7fb06346282",
                    type: "subitem",
                    value: "Profile",
                    link: "https://github.com/TomaszKandula?tab=overview",
                    icon: "Code",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "49d434fc-5ba7-4b90-8234-c3ec072d1727",
                    type: "subitem",
                    value: "Tokan Pages Project",
                    link: "https://github.com/TomaszKandula/TokanPages",
                    icon: "Code",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "58bbcb40-ce8c-45bc-843a-90ad023a86fd",
                    type: "subitem",
                    value: "Email Sender Project",
                    link: "https://github.com/TomaszKandula/EmailSender",
                    icon: "Code",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
            ],
        },
        {
            id: "6d8f8d4f-cbab-494e-8dc5-f1920b0bfdb6",
            type: "divider",
            value: "middle",
        },
        {
            id: "7c3f3893-2ac1-4ee8-94dc-baa44b668ec7",
            type: "item",
            value: "Tech Articles",
            link: "/articles",
            icon: "Menu_Book",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "5e8a29af-ada6-40dc-a07d-1c16d42cef87",
            type: "itemspan",
            value: "My interests",
            link: "#",
            icon: "Star",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
            subitems: [
                {
                    id: "eb7b1155-04c3-4dfe-b648-5f3e2432e6cc",
                    type: "subitem",
                    value: "Photography",
                    link: "/photography",
                    icon: "Photo_Camera",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "0b349f9f-2c69-446e-9861-314706a0d88f",
                    type: "subitem",
                    value: "Football",
                    link: "/football",
                    icon: "Sports_Soccer",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "827994a1-ce3b-4ff7-9452-e48fd4ccedae",
                    type: "subitem",
                    value: "Guitar",
                    link: "/guitar",
                    icon: "Music_Note",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "7a37afa9-a3a8-48f9-9694-95208f0e683a",
                    type: "subitem",
                    value: "Bicycle",
                    link: "/bicycle",
                    icon: "Directions_Bike",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
                {
                    id: "c3f5a67d-265b-4b86-98d3-2f4b2cc33083",
                    type: "subitem",
                    value: "Electronics",
                    link: "/electronics",
                    icon: "Electrical_Services",
                    enabled: true,
                    sideMenuOn: true,
                    navbarMenuOn: true,
                },
            ],
        },
        {
            id: "2455c0e2-da5e-4ba9-ac29-e87c82d998cc",
            type: "divider",
            value: "middle",
        },
        {
            id: "8495adf7-2b49-417e-83aa-6dcef4201ac4",
            type: "item",
            value: "Contact",
            link: "/contact",
            icon: "Contact_Mail",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "925cc29a-e0ad-4178-8492-813dedbcc563",
            type: "item",
            value: "About",
            link: "/about",
            icon: "Information_Circle",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: true,
        },
        {
            id: "8cfd30dd-f2ec-4f6d-80d5-e40b463f7348",
            type: "divider",
            value: "middle",
        },
        {
            id: "eb48d59c-a6a2-4526-914a-21caeb63ca1a",
            type: "item",
            value: "Terms of use",
            link: "/terms",
            icon: "Gavel",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: false,
        },
        {
            id: "3b54919c-837e-4efb-afd2-abd1c1d8b53b",
            type: "item",
            value: "Privacy policy",
            link: "/policy",
            icon: "Policy",
            enabled: true,
            sideMenuOn: true,
            navbarMenuOn: false,
        },
    ];

    const menuItems = render(
        <BrowserRouter>
            <RenderSideMenu isAnonymous={false} items={items}></RenderSideMenu>
        </BrowserRouter>
    );

    it("should return rendered list when items are provided.", () => {
        expect(menuItems).toMatchSnapshot();
    });
});
