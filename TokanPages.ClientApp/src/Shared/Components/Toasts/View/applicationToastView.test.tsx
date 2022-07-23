import "../../../../setupTests";
import React from "react";
import { shallow } from "enzyme";
import { ApplicationToastView } from "./applicationToastView";
import { Slide, SlideProps } from "@material-ui/core";

describe("Test view component for application toast.", () => 
{
    it("Renders correctly view component with passed props.", () => 
    {
        const vertical = "top";
        const horizontal = "right";
        const toastSeverity = "error";
        const autoHideDuration: number = 15000;

        const TransitionLeft = (props: Omit<SlideProps, "direction">) => <Slide {...props} direction="left" />;

        const tree = shallow(<ApplicationToastView bind=
        {{
            anchorOrigin: { vertical, horizontal },
            isOpen: false,
            autoHideDuration: autoHideDuration,
            closeEventHandler: jest.fn(),
            TransitionComponent: TransitionLeft,
            key: vertical + horizontal,
            toastSeverity: toastSeverity,
            toastMessage: "Test error message"
        }}/>);
        expect(tree).toMatchSnapshot();
    });
});
