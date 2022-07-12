import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import NavigationView from "../navigationView";
import { IItem } from "../../../Shared/Components/ListRender/Models/item";
import { ILanguage } from "../../../Shared/Services/languageService";

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

        const languages: ILanguage[] = 
        [
            { 
                id: "eng", 
                name: "English", 
                isDefault: true 
            },
            { 
                id: "pol", 
                name: "Polski", 
                isDefault: false 
            }
        ];

        const tree = shallow(<NavigationView bind=
        {{
            isLoading: false,
            drawerState: 
            { 
                open: false 
            },
            openHandler: jest.fn(),
            closeHandler: jest.fn(),
            infoHandler: jest.fn(),
            isAnonymous: false,
            logo: "",
            avatarName: "",
            userAliasText: "",
            menu: 
            { 
                image: "", 
                items: [items] 
            },
            languages: languages,
            selectedLanguage: "eng",
            languageHandler: jest.fn(),
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
