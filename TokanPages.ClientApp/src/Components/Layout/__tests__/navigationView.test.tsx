import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import NavigationView from "../navigationView";
import { IItem } from "../../../Shared/Components/ListRender/Models/item";

describe("Test component: featuresView.", () => 
{
    it("Renders correctly '<NavigationView />' when content is loaded.", () => 
    {
        const items: IItem = 
        {
            id: "79a6c65d-08b8-479b-9507-97feb05e30c2",
            type: "item",
            value: "Home",
            link: "/",
            icon: "Home",
            enabled: true
        };

        const tree = shallow(<NavigationView bind=
        {{
            drawerState: 
            { 
                open: false 
            },
            openHandler: jest.fn(),
            closeHandler: jest.fn(),
            isAnonymous: false,
            logo: "main.jpg",
            avatar: "default.jpg",
            anonymousText: "Anonymous",
            userAliasText: "",
            menu: 
            { 
                image: "", 
                items: [items] 
            }
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
