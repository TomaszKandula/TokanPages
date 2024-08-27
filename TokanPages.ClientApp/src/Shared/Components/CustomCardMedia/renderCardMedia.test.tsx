import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { RenderCardMedia } from "./customCardMedia";

describe("test rendering 'Custom Card Media' component", () => {
    interface Properties {
        basePath: string;
        imageSource: string;
        className: string;
    }

    const TestComponent = (props: Properties): JSX.Element => {
        return RenderCardMedia(props.basePath, props.imageSource, props.className);
    };

    it("should not render 'Custom Card Media' component.", () => {
        const html = render(<TestComponent basePath="" imageSource="" className="style" />);
        expect(html).toMatchSnapshot();
    });

    it("should render 'Custom Card Media' component.", () => {
        const html = render(
            <TestComponent basePath="http://domain.com/" imageSource="test-image.jpg" className="style" />
        );
        expect(html).toMatchSnapshot();
    });
});
