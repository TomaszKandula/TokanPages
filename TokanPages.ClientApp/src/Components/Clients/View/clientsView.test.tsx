import "../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { ClientsView } from "./clientsView";

describe("test component: clientsView", () => {
    it("should render correctly '<ClientsView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <ClientsView background="class-colour-white" />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
