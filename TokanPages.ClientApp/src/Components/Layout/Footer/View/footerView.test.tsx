import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import FooterView from "../View/footerView";
import { IIcon } from "../../../../Api/Models";

describe("Test component: footerView.", () => 
{
    it("Renders correctly '<FooterView />' when content is loaded.", () => 
    {
        const icons: IIcon = 
        {
            name: "LinkedInIcon",
            href: "https://www.linkedin.com/"
        };

        const tree = shallow(<FooterView bind=
        {{
            terms: 
            {
                text: "Terms of use",
                href: "/terms"
            },
            policy: 
            {
                text: "Privacy policy",
                href: "/policy"
            },
            versionInfo: "1.0",
            hasVersionInfo: false,
            backgroundColor: "#FFFFFF",
            copyright: "© 2020 - 2021 Tomasz Kandula",
            reserved: "All rights reserved",
            icons: [icons]
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
