import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { RenderImage } from "./customImage";

describe("test rendering image component", () => {
    it("should not render an image component.", () => {
        const html = render(<RenderImage basePath="" imageSource="" className="style" />);
        expect(html).toMatchSnapshot();
    });

    it("should render an image component.", () => {
        const html = render(
            <RenderImage basePath="http://localhost:5000/" imageSource="test-image.jpg" className="style" />
        );

        expect(html).toMatchSnapshot();
    });
});
