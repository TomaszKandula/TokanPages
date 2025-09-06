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
                    description={{
                        text: "",
                        label: "",
                        rows: 0,
                        required: true,
                        handler: jest.fn(),
                    }}
                    technology={{
                        caption: "Which technologies are you interested in?",
                        canDisplay: false,
                        handler: jest.fn(),
                        items: [
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
                        ],
                    }}
                    pricing={{
                        caption: "",
                        disclaimer: "",
                        serviceHandler: jest.fn(),
                        services: [
                            {
                                key: 0,
                                value: "Web Development (30 USD/hour)",
                                isChecked: false,
                            },
                            {
                                key: 1,
                                value: "Mobile Development (45 USD/hour)",
                                isChecked: false,
                            },
                        ],
                    }}
                    presentation={{
                        image: {
                            link: "",
                            title: "",
                            alt: "",
                            width: 0,
                            heigh: 0,
                        },
                        title: "",
                        subtitle: "",
                        icon: {
                            name: "",
                            href: "",
                        },
                        description: "",
                        logos: {
                            title: "",
                            images: [],
                        },
                    }}
                    className="mt-5 mb-5"
                    hasIcon={true}
                    hasCaption={true}
                    hasShadow={true}
                />
            </BrowserRouter>
        );

        expect(html).toMatchSnapshot();
    });
});
