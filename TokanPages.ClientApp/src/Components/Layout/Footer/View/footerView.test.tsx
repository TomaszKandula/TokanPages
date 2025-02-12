import "../../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { FooterView } from "../View/footerView";
import { IconDto } from "../../../../Api/Models";

describe("test component: footerView", () => {
    it("should render correctly '<FooterView />' when content is loaded.", () => {
        const icons: IconDto = {
            name: "LinkedInIcon",
            href: "https://www.linkedin.com/",
        };

        const html = render(
            <BrowserRouter>
                <FooterView
                    isLoading={false}
                    terms={{ text: "Terms of use", href: "/terms" }}
                    policy={{ text: "Privacy policy", href: "/policy" }}
                    versionInfo="1.0"
                    hasVersionInfo={false}
                    legalInfo={{
                        copyright: "© Tomasz Kandula",
                        reserved: "All rights reserved",
                    }}
                    hasLegalInfo={true}
                    icons={[icons]}
                    hasIcons={true}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
