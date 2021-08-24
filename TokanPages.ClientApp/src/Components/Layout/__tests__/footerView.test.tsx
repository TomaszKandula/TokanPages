import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import FooterView from "../footerView";
import { IFooterContentIconDto } from "../../../Api/Models";

describe("Test component: footerView.", () => 
{
    it("Renders correctly '<FooterView />' when content is loaded.", () => 
    {
        const icons: IFooterContentIconDto = 
        {
            name: "LinkedInIcon",
            link: "https://www.linkedin.com/"
        };

        const tree = shallow(<FooterView bind=
        {{
            terms: "Terms of use",
            policy: "Privacy policy",
            versionInfo: "1.0",
            hasVersionInfo: false,
            backgroundColor: "#FFFFFF",
            boxPaddingBottom: 0,
            copyright: "© 2020 - 2021 Tomasz Kandula",
            reserved: "All rights reserved",
            icons: [icons]
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
