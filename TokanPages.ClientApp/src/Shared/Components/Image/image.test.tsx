import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { Image } from "./image";

describe("test rendering image component", () => {
    it("should not render an image component.", () => {
        const html = render(<Image base="" source="" className="style" />);
        expect(html).toMatchSnapshot();
    });

    it("should render an image component.", () => {
        const html = render(
            <Image
                base="http://localhost:5000/"
                source="test-image.jpg"
                alt="An article test image"
                className="style"
            />
        );

        expect(html).toMatchSnapshot();
    });
});
