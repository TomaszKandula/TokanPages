import * as React from "react";

const useIsomorphicLayoutEffect = typeof window !== "undefined" ? React.useLayoutEffect : React.useEffect;

export const useInterval = (callback: () => void, delay: number | null) =>
{
    const savedCallback = React.useRef(callback)

    useIsomorphicLayoutEffect(() => 
    {
        savedCallback.current = callback
    }, 
    [ callback ]);

    React.useEffect(() => 
    {
        if (!delay && delay !== 0) 
        {
            return;
        }

        const id = setInterval(() => savedCallback.current(), delay);

        return () => clearInterval(id);
    }, 
    [ delay ]);
}
