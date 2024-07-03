import "../../../../setupTests";
import React from "react";
import { render } from "enzyme";
import { FooterView } from "../View/footerView";
import { IconDto } from "../../../../Api/Models";

describe("test component: footerView", () => {
    it("should render correctly '<FooterView />' when content is loaded.", () => {
        const icons: IconDto = {
            name: "LinkedInIcon",
            href: "https://www.linkedin.com/",
        };

        const html = render(
            <FooterView
                terms={{ text: "Terms of use", href: "/terms" }}
                policy={{ text: "Privacy policy", href: "/policy" }}
                versionInfo="1.0"
                hasVersionInfo={false}
                backgroundColor="#FFFFFF"
                copyright="© 2020 - 2023 Tomasz Kandula"
                reserved="All rights reserved"
                icons={[icons]}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
