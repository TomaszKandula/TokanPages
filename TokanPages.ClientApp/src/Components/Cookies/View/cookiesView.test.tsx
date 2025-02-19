import "../../../setupTests";
import React from "react";
import { render } from "@testing-library/react";
import { CookiesView } from "./cookiesView";

describe("test component: cookiesView", () => {
    it("should render correctly '<CookiesView />' when content is loaded.", () => {
        const html = render(
            <CookiesView
                isLoading={false}
                isClose={false}
                hasSnapshotMode={false}
                hasCookieConsent={false}
                caption="Cookie Policy"
                text="We use cookies to personalise content..."
                detail="With your consent, we may also..."
                loading={["Loading...", "Please wait..."]}
                canShowOptions={false}
                onAcceptButtonEvent={jest.fn()}
                onManageButtonEvent={jest.fn()}
                onCloseButtonEvent={jest.fn()}
                onStatisticsCheckboxEvent={jest.fn()}
                onMarketingCheckboxEvent={jest.fn()}
                onPersonalizationCheckboxEvent={jest.fn()}
                hasStatistics={false}
                hasMarketing={false}
                hasPersonalization={false}
                options={{
                    enabled: true,
                    necessaryLabel: "Necessary",
                    statisticsLabel: "Statistics",
                    marketingLabel: "Marketing",
                    personalizationLabel: "Personalization",
                }}
                buttons={{
                    acceptButton: {
                        label: "Accept All",
                        enabled: true,
                    },
                    manageButton: {
                        label: "Manage Settings",
                        enabled: true,
                    },
                    closeButton: {
                        label: "Close",
                        enabled: true,
                    },
                }}
            />
        );

        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<CookiesView />' when content is loaded w/options.", () => {
        const html = render(
            <CookiesView
                isLoading={false}
                isClose={false}
                hasSnapshotMode={false}
                hasCookieConsent={false}
                caption="Cookie Policy"
                text="We use cookies to personalise content..."
                detail="With your consent, we may also..."
                loading={["Loading...", "Please wait..."]}
                canShowOptions={true}
                onAcceptButtonEvent={jest.fn()}
                onManageButtonEvent={jest.fn()}
                onCloseButtonEvent={jest.fn()}
                onStatisticsCheckboxEvent={jest.fn()}
                onMarketingCheckboxEvent={jest.fn()}
                onPersonalizationCheckboxEvent={jest.fn()}
                hasStatistics={false}
                hasMarketing={false}
                hasPersonalization={false}
                options={{
                    enabled: true,
                    necessaryLabel: "Necessary",
                    statisticsLabel: "Statistics",
                    marketingLabel: "Marketing",
                    personalizationLabel: "Personalization",
                }}
                buttons={{
                    acceptButton: {
                        label: "Accept All",
                        enabled: true,
                    },
                    manageButton: {
                        label: "Manage Settings",
                        enabled: true,
                    },
                    closeButton: {
                        label: "Close",
                        enabled: true,
                    },
                }}
            />
        );

        expect(html).toMatchSnapshot();
    });

    it("should render correctly '<CookiesView />' when content is loading.", () => {
        const html = render(
            <CookiesView
                isLoading={true}
                isClose={false}
                hasSnapshotMode={false}
                hasCookieConsent={false}
                caption="Cookie Policy"
                text="We use cookies to personalise content..."
                detail="With your consent, we may also..."
                loading={["Loading...", "Please wait..."]}
                canShowOptions={false}
                onAcceptButtonEvent={jest.fn()}
                onManageButtonEvent={jest.fn()}
                onCloseButtonEvent={jest.fn()}
                onStatisticsCheckboxEvent={jest.fn()}
                onMarketingCheckboxEvent={jest.fn()}
                onPersonalizationCheckboxEvent={jest.fn()}
                hasStatistics={false}
                hasMarketing={false}
                hasPersonalization={false}
                options={{
                    enabled: true,
                    necessaryLabel: "Necessary",
                    statisticsLabel: "Statistics",
                    marketingLabel: "Marketing",
                    personalizationLabel: "Personalization",
                }}
                buttons={{
                    acceptButton: {
                        label: "Accept All",
                        enabled: true,
                    },
                    manageButton: {
                        label: "Manage Settings",
                        enabled: true,
                    },
                    closeButton: {
                        label: "Close",
                        enabled: true,
                    },
                }}
            />
        );

        expect(html).toMatchSnapshot();
    });
});
