import "../../../setupTests";
import React from "react";
import * as Redux from "react-redux";
import { shallow } from "enzyme";
import { UserSignup } from "./userSignup";

jest.mock("react-redux", () => ({
    ...jest.requireActual("react-redux"),
    useSelector: jest.fn()
}));

describe("test account group component: userSignup", () => {
    const testContent = {
        language: "",
        caption: "",
        button: "",
        link: "",
        warning: "",
        consent: "",
        labelFirstName: "",
        labelLastName: "",
        labelEmail: "",
        labelPassword: ""
    };

    const useDispatchMock = jest.spyOn(Redux, "useDispatch");
    const wrapper = shallow(
        <div>
            <UserSignup />
        </div>
    );

    beforeEach(() => {
        jest.spyOn(Redux, "useSelector").mockReturnValueOnce({
            isLoading: false,
            content: testContent
        });

        wrapper.find("UserSignup").dive();
    });

    it("should render correctly '<UserSignup />' when content is loaded.", () => {
        useDispatchMock.mockReturnValue(jest.fn());
        expect(useDispatchMock).toBeCalledTimes(1);
        expect(wrapper).toMatchSnapshot();
    });
});
