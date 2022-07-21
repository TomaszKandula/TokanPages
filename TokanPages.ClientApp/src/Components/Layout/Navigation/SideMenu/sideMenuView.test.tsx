import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import SideMenuView from "./sideMenuView";
import { IItem } from "../../../../Shared/Components/ListRender/Models/item";

describe("Test component: menuView.", () => 
{
    it("Renders correctly '<MenuView />' when content is loaded.", () => 
    {
        const item: IItem = 
        {
            id: "8c678e9e-699e-41c7-a93a-587c6a4f41e7",
            type: "item",
            value: "Sign in",
            link: "/signin",
            icon: "VpnKey",
            enabled: true
        };

        const tree = shallow(<SideMenuView bind=
        {{
            drawerState: 
            { 
                open: false 
            },
            closeHandler: jest.fn(),
            isAnonymous: true,
            menu: 
            { 
                image: "",
                items: [item]
            }
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
