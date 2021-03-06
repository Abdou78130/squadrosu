// Copyright (c) SquadroCorp
// This file is part of Squadrosu!.
// Squadrosu! is licensed under the GPL v3. See LICENSE.md for details.

using osu.Framework.Graphics;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Platform;
using osu.Framework.Testing;

namespace Squadrosu.Game.Tests;

public class SquadrosuTestBrowser : SquadrosuGameBase
{
    protected override void LoadComplete()
    {
        base.LoadComplete();

        AddRange(new Drawable[]
        {
            new TestBrowser("Squadrosu"),
            new CursorContainer()
        });
    }

    public override void SetHost(GameHost host)
    {
        base.SetHost(host);
        host.Window.CursorState |= CursorState.Hidden;
    }
}
