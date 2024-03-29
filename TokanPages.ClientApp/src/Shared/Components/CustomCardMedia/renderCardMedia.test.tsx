import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
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
        const tree = shallow(<TestComponent basePath="" imageSource="" className="style" />);
        expect(tree).toMatchSnapshot();
    });

    it("should render 'Custom Card Media' component.", () => {
        const tree = shallow(
            <TestComponent basePath="http://domain.com/" imageSource="test-image.jpg" className="style" />
        );
        expect(tree).toMatchSnapshot();
    });
});
