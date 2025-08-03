import { DesktopOnlyMedia, type DesktopOnlyMediaProps } from "./desktopOnlyMedia";
import { MobileOnlyMedia, type MobileOnlyMediaProps } from "./mobileOnlyMedia";
import { TabletOnlyMedia, type TabletOnlyMediaProps } from "./tabletOnlyMedia";

interface MediaProps {
    DesktopOnly: (props: DesktopOnlyMediaProps) => React.ReactElement;
    TabletOnly: (props: TabletOnlyMediaProps) => React.ReactElement;
    MobileOnly: (props: MobileOnlyMediaProps) => React.ReactElement;
}

export const Media: MediaProps = {
    DesktopOnly: DesktopOnlyMedia,
    TabletOnly: TabletOnlyMedia,
    MobileOnly: MobileOnlyMedia,
};
