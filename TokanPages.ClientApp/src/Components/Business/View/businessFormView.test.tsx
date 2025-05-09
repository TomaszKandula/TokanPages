import "../../../setupTests";
import React from "react";
import { BrowserRouter } from "react-router-dom";
import { render } from "@testing-library/react";
import { BusinessFormView } from "./businessFormView";

describe("test component: businessFormView", () => {
    it("should render correctly '<BusinessFormView />' when content is loaded.", () => {
        const html = render(
            <BrowserRouter>
                <BusinessFormView
                    isLoading={false}
                    caption="Business Inquiry"
                    progress={false}
                    buttonText="Submit"
                    keyHandler={jest.fn()}
                    formHandler={jest.fn()}
                    buttonHandler={jest.fn()}
                    techHandler={jest.fn()}
                    serviceHandler={jest.fn()}
                    serviceSelection={["ABC", "CDE"]}
                    companyText=""
                    companyLabel="Company name"
                    firstNameText=""
                    firstNameLabel="First name"
                    lastNameText=""
                    lastNameLabel="Last name"
                    emailText=""
                    emailLabel="Email"
                    phoneText=""
                    phoneLabel="Business phone"
                    techLabel="Which technologies are you interested in?"
                    techItems={[
                        {
                            value: "ABC",
                            key: 1,
                            isChecked: false,
                        },
                        {
                            value: "CDE",
                            key: 2,
                            isChecked: false,
                        },
                    ]}
                    description={{
                        text: "",
                        label: "",
                        multiline: false,
                        rows: 0,
                        required: true,
                    }}
                    pricing={{
                        caption: "",
                        disclaimer: "",
                        services: [
                            {
                                id: "web",
                                text: "Web Development",
                                price: "30 USD/h",
                            },
                            {
                                id: "mobile",
                                text: "Mobile Development",
                                price: "45 USD/h",
                            },
                        ],
                    }}
                    className="mt-15 mb-15"
                    background="class-colour-white"
                    hasIcon={true}
                    hasCaption={true}
                    hasShadow={true}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
