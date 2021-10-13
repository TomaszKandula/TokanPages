import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { RenderList } from "../renderList";
import { IItem } from "../Models/item";

describe("Test render function 'renderList'.", () => 
{
    const noItems = shallow(<RenderList bind={{ isAnonymous: true, items: undefined }}></RenderList>);

    it("Should return 'Cannot render content.' when called with items undefined.", () => 
    {
        expect(noItems).toMatchSnapshot();
    });

    const emptyItems = shallow(<RenderList bind={{ isAnonymous: true, items: [] }}></RenderList>);

    it("Should return 'Cannot render content.' when called with empty array of items.", () => 
    {
        expect(emptyItems).toMatchSnapshot();
    });

    const items: IItem[] = 
    [
        {
            id: "5d762733-6724-4b4f-bc8c-7ede6baf77d5",
            type: "item",
            value: "Sign in",
            link: "/signin",
            icon: "VpnKey",
            enabled: true
        },
        {
            id: "bdd33e5c-f942-4b14-96c7-16523f693c1c",
            type: "item",
            value: "Sign up",
            link: "/signup",
            icon: "PersonAdd",
            enabled: true
        },
        {
            id: "e8284fe7-5ced-46a5-b779-2fe516fe88c2",
            type: "item",
            value: "My account",
            link: "/account",
            icon: "Person",
            enabled: true
        },	
        {
            id: "d3ada751-012d-4c58-a4dd-a89c89928338",
            type: "item",
            value: "Sign out",
            link: "/signout",
            icon: "Lock",
            enabled: true
        },
        {
            id: "945b6c9c-f37f-4b03-bb6f-3256a559fafe",
            type: "divider",
            value: "middle"
        },				
        {
            id: "c44bb2cd-ee75-470c-942e-5560e3589102",
            type: "item",
            value: "Home",
            link: "/",
            icon: "Home",
            enabled: true
        },
        {
            id: "7c3f3893-2ac1-4ee8-94dc-baa44b668ec7",
            type: "item",
            value: "Articles",
            link: "/articles",
            icon: "ViewList",
            enabled: true
        },
        {
            id: "d5303362-76fb-4638-a6ee-5077cac76826",
            type: "itemspan",
            value: "My projects",
            link: "#",
            icon: "Build",
            enabled: true,
            subitems: 
            [
                {
                    id: "49d434fc-5ba7-4b90-8234-c3ec072d1727",
                    type: "subitem",
                    value: "VAT Validation",
                    link: "#",
                    icon: "Assignment",
                    enabled: false
                }
            ]
        },
        {
            id: "5e8a29af-ada6-40dc-a07d-1c16d42cef87",
            type: "itemspan",
            value: "My interests",
            link: "#",
            icon: "Star",
            enabled: true,
            subitems: 
            [
                {
                    "id": "eb7b1155-04c3-4dfe-b648-5f3e2432e6cc",
                    "type": "subitem",
                    "value": "Photography",
                    "link": "/photography",
                    "icon": "PhotoCamera",
                    "enabled": false
                },
                {
                    "id": "0b349f9f-2c69-446e-9861-314706a0d88f",
                    "type": "subitem",
                    "value": "Football",
                    "link": "/football",
                    "icon": "SportsSoccer",
                    "enabled": false
                }
            ]
        },
        {
            id: "8495adf7-2b49-417e-83aa-6dcef4201ac4",
            type: "This is not valid object",
            value: "",
            link: "",
            icon: ""
        },
        {
            id: "2455c0e2-da5e-4ba9-ac29-e87c82d998cc",
            type: "divider",
            value: "middle"
        },				
        {
            id: "3b54919c-837e-4efb-afd2-abd1c1d8b53b",
            type: "item",
            value: "Privacy policy",
            link: "/policy",
            icon: "Policy",
            enabled: true
        }
    ];

    const menuItems = shallow(<RenderList bind={{ isAnonymous: false, items: items }}></RenderList>);

    it("Should return rendered list when items are provided.", () => 
    {
        expect(menuItems).toMatchSnapshot();
    });
});
