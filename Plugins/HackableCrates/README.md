# HackableCrates

The HackableCrates plugin provides easily configurable functionality to Rust's time-based hackable crates.

## Default (Plugin) Configuration

Below is the default plugin configuration settings should you need to reset your configuration.

```json
{
  "(1). Enable the HackableCrates plugin?": true,
  "(2). Enable a non-default crate timer?": false,
  "(3). If enabled, set the custom timer's time (minutes).": 15.0,
  "(4). Revert unlock time to default on plugin unload?": true,
  "(5). Announce when a crate starts to be unlocked?": false,
  "(6). Announce when a crate has finished unlocking?": false
}
```

## Default (Language) Configuration

Below is the default language configuration settings should you need to reset your language file.

```json
{
  "NewUnlockEvent": "A hackable crate has begun to be unlocked!",
  "EndUnlockEvent": "A hackable crate has just unlocked!"
}
```

To use colour codes use `[#HEXCODE]Text[/#]` to do such; for example, `[#123ABC]Text[/#]`.

## MIT License

Copyright © 2021 ReeceDeveloper

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
documentation files (the “Software”), to deal in the Software without restriction, including without limitation
the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
DEALINGS IN THE SOFTWARE.