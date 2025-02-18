import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { RenderImage } from "./customImage";

describe("test rendering image component", () => {
    it("should not render an image component.", () => {
        const html = render(<RenderImage base="" source="" className="style" />);
        expect(html).toMatchSnapshot();
    });

    it("should render an image component.", () => {
        const html = render(
            <RenderImage base="http://localhost:5000/" source="test-image.jpg" className="style" />
        );

        expect(html).toMatchSnapshot();
    });
});
