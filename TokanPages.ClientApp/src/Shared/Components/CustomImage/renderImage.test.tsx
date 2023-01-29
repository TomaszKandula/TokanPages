import "../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { RenderImage } from "./customImage"; 

describe("Test rendering image component.", () => 
{
    interface Properties 
    {
        basePath: string; 
        imageSource: string; 
        className: string;
    }

    const TestComponent = (props: Properties): JSX.Element | null => 
    {
        return RenderImage(props.basePath, props.imageSource, props.className);
    }

    it("Should not render an image component.", () => 
    {
        const tree = shallow(<TestComponent 
            basePath=""
            imageSource=""
            className="style"
        />);
        expect(tree).toMatchSnapshot();
    });

    it("Should render an image component.", () => 
    {
        const tree = shallow(<TestComponent 
            basePath="http://localhost:5000/"
            imageSource="test-image.jpg"
            className="style"        
        />);
        expect(tree).toMatchSnapshot();
    });
});
