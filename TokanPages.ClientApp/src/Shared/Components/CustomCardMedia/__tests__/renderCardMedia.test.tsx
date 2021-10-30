import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { renderCardMedia } from "../customCardMedia";

describe("Test rendering 'Custom Card Media' component.", () => 
{
    interface IProperties 
    {
        imageSource: string; 
        className: string;
    }

    const TestComponent = (props: IProperties): JSX.Element => 
    {
        return renderCardMedia(props.imageSource, props.className);
    }

    it("Should not render 'Custom Card Media' component.", () => 
    {
        const tree = shallow(<TestComponent 
            imageSource=""
            className="style"
        />);
        expect(tree).toMatchSnapshot();
    });

    it("Should render 'Custom Card Media' component.", () => 
    {
        const tree = shallow(<TestComponent 
            imageSource="test-image.jpg"
            className="style"        
        />);
        expect(tree).toMatchSnapshot();
    });
});
