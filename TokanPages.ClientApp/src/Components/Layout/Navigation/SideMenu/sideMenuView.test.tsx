import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { SideMenuView } from "./sideMenuView";
import { Item } from "../../../../Shared/Components/ListRender/Models";

describe("test component: menuView", () => 
{
    it("should render correctly '<MenuView />' when content is loaded.", () => 
    {
        const item: Item = 
        {
            id: "8c678e9e-699e-41c7-a93a-587c6a4f41e7",
            type: "item",
            value: "Sign in",
            link: "/signin",
            icon: "VpnKey",
            enabled: true
        };

        const tree = shallow(<SideMenuView
            drawerState={{ open: false }}
            closeHandler={jest.fn()}
            isAnonymous={true}
            menu={{ image: "", items: [item] }}
        />);

        expect(tree).toMatchSnapshot();
    });
});
