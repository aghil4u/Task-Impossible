﻿/*
 * arrive.js
 * v2.3.1
 * https://github.com/uzairfarooq/arrive
 * MIT licensed
 *
 * Copyright (c) 2014-2016 Uzair Farooq
 */


/*This will dynamically add elements to your DOM and automatically apply material.js to every new element *
added via JavaScript*/
var Arrive = function(a, b, c) {
    "use strict";

    function l(a, b, c) {
        e.addMethod(b, c, a.unbindEvent), e.addMethod(b, c, a.unbindEventWithSelectorOrCallback), e.addMethod(b,
            c,
            a.unbindEventWithSelectorAndCallback);
    }

    function m(a) { a.arrive = j.bindEvent, l(j, a, "unbindArrive"), a.leave = k.bindEvent, l(k, a, "unbindLeave") }

    if (a.MutationObserver && "undefined" != typeof HTMLElement) {
        var d = 0,
            e = function() {
                var b = HTMLElement.prototype.matches ||
                    HTMLElement.prototype.webkitMatchesSelector ||
                    HTMLElement.prototype.mozMatchesSelector ||
                    HTMLElement.prototype.msMatchesSelector;
                return {
                    matchesSelector: function(a, c) { return a instanceof HTMLElement && b.call(a, c) },
                    addMethod: function(a, b, c) {
                        var d = a[b];
                        a[b] = function() {
                            return c.length == arguments.length
                                ? c.apply(this, arguments)
                                : "function" == typeof d
                                ? d.apply(this, arguments)
                                : void 0;
                        };
                    },
                    callCallbacks: function(a) { for (var c, b = 0; c = a[b]; b++) c.callback.call(c.elem) },
                    checkChildNodesRecursively: function(a, b, c, d) {
                        for (var g, f = 0; g = a[f]; f++)
                            c(g, b, d) && d.push({ callback: b.callback, elem: g }), g.childNodes.length > 0 &&
                                e.checkChildNodesRecursively(g.childNodes, b, c, d);
                    },
                    mergeArrays: function(a, b) {
                        var d, c = {};
                        for (d in a) c[d] = a[d];
                        for (d in b) c[d] = b[d];
                        return c;
                    },
                    toElementsArray: function(b) {
                        return "undefined" == typeof b || "number" == typeof b.length && b !== a || (b = [b]), b;
                    }
                };
            }(),
            f = function() {
                var a = function() { this._eventsBucket = [], this._beforeAdding = null, this._beforeRemoving = null };
                return a.prototype.addEvent = function(a, b, c, d) {
                    var e = { target: a, selector: b, options: c, callback: d, firedElems: [] };
                    return this._beforeAdding && this._beforeAdding(e), this._eventsBucket.push(e), e;
                }, a.prototype.removeEvent =
                    function(a) {
                        for (var c, b = this._eventsBucket.length - 1; c = this._eventsBucket[b]; b--)
                            a(c) && (this._beforeRemoving && this._beforeRemoving(c), this._eventsBucket.splice(b, 1));
                    }, a.prototype.beforeAdding = function(a) { this._beforeAdding = a }, a.prototype.beforeRemoving =
                    function(a) { this._beforeRemoving = a }, a;
            }(),
            g = function(b, d) {
                var g = new f, h = this, i = { fireOnAttributesModification: !1 };
                return g.beforeAdding(function(c) {
                    var i, e = c.target;
                    c.selector, c.callback;
                    (e === a.document || e === a) && (e = document.getElementsByTagName("html")[0]), i =
                        new MutationObserver(function(a) { d.call(this, a, c) });
                    var j = b(c.options);
                    i.observe(e, j), c.observer = i, c.me = h;
                }), g.beforeRemoving(function(a) { a.observer.disconnect() }), this.bindEvent = function(a, b, c) {
                    b = e.mergeArrays(i, b);
                    for (var d = e.toElementsArray(this), f = 0; f < d.length; f++) g.addEvent(d[f], a, b, c);
                }, this.unbindEvent = function() {
                    var a = e.toElementsArray(this);
                    g.removeEvent(function(b) {
                        for (var d = 0; d < a.length; d++) if (this === c || b.target === a[d]) return !0;
                        return !1;
                    });
                }, this.unbindEventWithSelectorOrCallback = function(a) {
                    var f, b = e.toElementsArray(this), d = a;
                    f = "function" == typeof a
                        ? function(a) {
                            for (var e = 0; e < b.length; e++)
                                if ((this === c || a.target === b[e]) && a.callback === d) return !0;
                            return !1;
                        }
                        : function(d) {
                            for (var e = 0; e < b.length; e++)
                                if ((this === c || d.target === b[e]) && d.selector === a) return !0;
                            return !1;
                        }, g.removeEvent(f);
                }, this.unbindEventWithSelectorAndCallback = function(a, b) {
                    var d = e.toElementsArray(this);
                    g.removeEvent(function(e) {
                        for (var f = 0; f < d.length; f++)
                            if ((this === c || e.target === d[f]) && e.selector === a && e.callback === b) return !0;
                        return !1;
                    });
                }, this;
            },
            h = function() {
                function h(a) {
                    var b = { attributes: !1, childList: !0, subtree: !0 };
                    return a.fireOnAttributesModification && (b.attributes = !0), b;
                }

                function i(a, b) {
                    a.forEach(function(a) {
                        var c = a.addedNodes, d = a.target, f = [];
                        null !== c && c.length > 0
                            ? e.checkChildNodesRecursively(c, b, k, f)
                            : "attributes" === a.type && k(d, b, f) && f.push({ callback: b.callback, elem: node }), e
                            .callCallbacks(f);
                    });
                }

                function k(a, b, f) {
                    if (e.matchesSelector(a, b.selector) &&
                        (a._id === c && (a._id = d++), -1 == b.firedElems.indexOf(a._id))) {
                        if (b.options.onceOnly) {
                            if (0 !== b.firedElems.length) return;
                            b.me.unbindEventWithSelectorAndCallback.call(b.target, b.selector, b.callback);
                        }
                        b.firedElems.push(a._id), f.push({ callback: b.callback, elem: a });
                    }
                }

                var f = { fireOnAttributesModification: !1, onceOnly: !1, existing: !1 };
                j = new g(h, i);
                var l = j.bindEvent;
                return j.bindEvent = function(a, b, c) {
                    "undefined" == typeof c ? (c = b, b = f) : b = e.mergeArrays(f, b);
                    var d = e.toElementsArray(this);
                    if (b.existing) {
                        for (var g = [], h = 0; h < d.length; h++)
                            for (var i = d[h].querySelectorAll(a), j = 0; j < i.length; j++)
                                g.push({ callback: c, elem: i[j] });
                        if (b.onceOnly && g.length) return c.call(g[0].elem);
                        setTimeout(e.callCallbacks, 1, g);
                    }
                    l.call(this, a, b, c);
                }, j;
            },
            i = function() {
                function d(a) {
                    var b = { childList: !0, subtree: !0 };
                    return b;
                }

                function f(a, b) {
                    a.forEach(function(a) {
                        var c = a.removedNodes, f = (a.target, []);
                        null !== c && c.length > 0 && e.checkChildNodesRecursively(c, b, h, f), e.callCallbacks(f);
                    });
                }

                function h(a, b) { return e.matchesSelector(a, b.selector) }

                var c = {};
                k = new g(d, f);
                var i = k.bindEvent;
                return k.bindEvent = function(a, b, d) {
                    "undefined" == typeof d ? (d = b, b = c) : b = e.mergeArrays(c, b), i.call(this, a, b, d);
                }, k;
            },
            j = new h,
            k = new i;
        b && m(b.fn), m(HTMLElement.prototype), m(NodeList.prototype), m(HTMLCollection.prototype),
            m(HTMLDocument.prototype), m(Window.prototype);
        var n = {};
        return l(j, n, "unbindAllArrive"), l(k, n, "unbindAllLeave"), n;
    }
}(window, "undefined" == typeof jQuery ? null : jQuery, void 0);


//Bootstrap Material Design https://github.com/FezVrasta/bootstrap-material-design#arrivejs-support
!function(t) {
    function o(t) {
        return "undefined" == typeof t.which
            ? !0
            : "number" == typeof t.which && t.which > 0
            ? !t.ctrlKey &&
            !t.metaKey &&
            !t.altKey &&
            8 != t.which &&
            9 != t.which &&
            13 != t.which &&
            16 != t.which &&
            17 != t.which &&
            20 != t.which &&
            27 != t.which
            : !1;
    }

    function i(o) {
        var i = t(o);
        i.prop("disabled") || i.closest(".form-group").addClass("is-focused");
    }

    function n(o) {
        o.closest("label").hover(function() {
                var o = t(this).find("input");
                o.prop("disabled") || i(o);
            },
            function() { e(t(this).find("input")) });
    }

    function e(o) { t(o).closest(".form-group").removeClass("is-focused") }

    t.expr[":"].notmdproc = function(o) { return t(o).data("mdproc") ? !1 : !0 }, t.material = {
        options: {
            validate: !0,
            input: !0,
            ripples: !0,
            checkbox: !0,
            togglebutton: !0,
            radio: !0,
            arrive: !0,
            autofill: !1,
            withRipples: [
                ".btn:not(.btn-link)", ".card-image", ".navbar a:not(.withoutripple)", ".footer a:not(.withoutripple)",
                ".dropdown-menu a", ".nav-tabs a:not(.withoutripple)", ".withripple",
                ".pagination li:not(.active):not(.disabled) a:not(.withoutripple)"
            ].join(","),
            inputElements: "input.form-control, textarea.form-control, select.form-control",
            checkboxElements: ".checkbox > label > input[type=checkbox]",
            togglebuttonElements: ".togglebutton > label > input[type=checkbox]",
            radioElements: ".radio > label > input[type=radio]"
        },
        checkbox: function(o) {
            var i = t(o ? o : this.options.checkboxElements).filter(":notmdproc").data("mdproc", !0)
                .after("<span class='checkbox-material'><span class='check'></span></span>");
            n(i);
        },
        togglebutton: function(o) {
            var i = t(o ? o : this.options.togglebuttonElements).filter(":notmdproc").data("mdproc", !0)
                .after("<span class='toggle'></span>");
            n(i);
        },
        radio: function(o) {
            var i = t(o ? o : this.options.radioElements).filter(":notmdproc").data("mdproc", !0)
                .after("<span class='circle'></span><span class='check'></span>");
            n(i);
        },
        input: function(o) {
            t(o ? o : this.options.inputElements).filter(":notmdproc").data("mdproc", !0).each(function() {
                var o = t(this), i = o.closest(".form-group");
                0 === i.length && (o.wrap("<div class='form-group'></div>"), i = o.closest(".form-group")),
                    o.attr("data-hint") &&
                        (o.after("<p class='help-block'>" + o.attr("data-hint") + "</p>"), o.removeAttr("data-hint"));
                var n = { "input-lg": "form-group-lg", "input-sm": "form-group-sm" };
                if (t.each(n, function(t, n) { o.hasClass(t) && (o.removeClass(t), i.addClass(n)) }), o.hasClass(
                    "floating-label")) {
                    var e = o.attr("placeholder");
                    o.attr("placeholder", null).removeClass("floating-label");
                    var a = o.attr("id"), r = "";
                    a && (r = "for='" + a + "'"), i.addClass("label-floating"), o.after("<label " +
                        r +
                        "class='control-label'>" +
                        e +
                        "</label>");
                }
                (null === o.val() || "undefined" == o.val() || "" === o.val()) && i.addClass("is-empty"),
                    i.append("<span class='material-input'></span>"), i.find("input[type=file]").length > 0 &&
                        i.addClass("is-fileinput");
            });
        },
        attachInputEventHandlers: function() {
            var n = this.options.validate;
            t(document).on("change", ".checkbox input[type=checkbox]", function() { t(this).blur() })
                .on("keydown paste",
                    ".form-control",
                    function(i) { o(i) && t(this).closest(".form-group").removeClass("is-empty") }).on("keyup change",
                    ".form-control",
                    function() {
                        var o = t(this),
                            i = o.closest(".form-group"),
                            e = "undefined" == typeof o[0].checkValidity || o[0].checkValidity();
                        "" === o.val() ? i.addClass("is-empty") : i.removeClass("is-empty"), n &&
                            (e ? i.removeClass("has-error") : i.addClass("has-error"));
                    }).on("focus", ".form-control, .form-group.is-fileinput", function() { i(this) })
                .on("blur", ".form-control, .form-group.is-fileinput", function() { e(this) }).on("change",
                    ".form-group input",
                    function() {
                        var o = t(this);
                        if ("file" != o.attr("type")) {
                            var i = o.closest(".form-group"), n = o.val();
                            n ? i.removeClass("is-empty") : i.addClass("is-empty");
                        }
                    }).on("change",
                    ".form-group.is-fileinput input[type='file']",
                    function() {
                        var o = t(this), i = o.closest(".form-group"), n = "";
                        t.each(this.files, function(t, o) { n += o.name + ", " }), n =
                            n.substring(0, n.length - 2), n ? i.removeClass("is-empty") : i.addClass("is-empty"), i
                            .find("input.form-control[readonly]").val(n);
                    });
        },
        ripples: function(o) { t(o ? o : this.options.withRipples).ripples() },
        autofill: function() {
            var o = setInterval(function() {
                    t("input[type!=checkbox]").each(function() {
                        var o = t(this);
                        o.val() && o.val() !== o.attr("value") && o.trigger("change");
                    });
                },
                100);
            setTimeout(function() { clearInterval(o) }, 1e4);
        },
        attachAutofillEventHandlers: function() {
            var o;
            t(document).on("focus",
                "input",
                function() {
                    var i = t(this).parents("form").find("input").not("[type=file]");
                    o = setInterval(function() {
                            i.each(function() {
                                var o = t(this);
                                o.val() !== o.attr("value") && o.trigger("change");
                            });
                        },
                        100);
                }).on("blur", ".form-group input", function() { clearInterval(o) });
        },
        init: function(o) {
            this.options = t.extend({}, this.options, o);
            var i = t(document);
            t.fn.ripples && this.options.ripples && this.ripples(),
                this.options.input && (this.input(), this.attachInputEventHandlers()),
                this.options.checkbox && this.checkbox(), this.options.togglebutton && this.togglebutton(),
                this.options.radio && this.radio(),
                this.options.autofill && (this.autofill(), this.attachAutofillEventHandlers()), document.arrive &&
                    this.options.arrive &&
                    (t.fn.ripples &&
                            this.options.ripples &&
                            i.arrive(this.options.withRipples, function() { t.material.ripples(t(this)) }),
                        this.options.input &&
                            i.arrive(this.options.inputElements, function() { t.material.input(t(this)) }),
                        this.options.checkbox &&
                            i.arrive(this.options.checkboxElements, function() { t.material.checkbox(t(this)) }),
                        this.options.radio &&
                            i.arrive(this.options.radioElements, function() { t.material.radio(t(this)) }),
                        this.options.togglebutton &&
                            i.arrive(this.options.togglebuttonElements,
                                function() { t.material.togglebutton(t(this)) }));
        }
    };
}(jQuery), function(t, o, i, n) {
    "use strict";

    function e(o, i) {
        r = this, this.element = t(o), this.options = t.extend({}, s, i), this._defaults = s, this._name =
            a, this.init();
    }

    var a = "ripples", r = null, s = {};
    e.prototype.init = function() {
            var i = this.element;
            i.on("mousedown touchstart",
                function(n) {
                    if (!r.isTouch() || "mousedown" !== n.type) {
                        i.find(".ripple-container").length || i.append('<div class="ripple-container"></div>');
                        var e = i.children(".ripple-container"), a = r.getRelY(e, n), s = r.getRelX(e, n);
                        if (a || s) {
                            var l = r.getRipplesColor(i), p = t("<div></div>");
                            p.addClass("ripple").css({ left: s, top: a, "background-color": l }), e.append(p),
                                function() {
                                    return o.getComputedStyle(p[0]).opacity;
                                }(), r.rippleOn(i, p), setTimeout(function() { r.rippleEnd(p) }, 500), i.on(
                                    "mouseup mouseleave touchend",
                                    function() {
                                        p.data("mousedown", "off"), "off" === p.data("animating") && r.rippleOut(p);
                                    });
                        }
                    }
                });
        }, e.prototype.getNewSize =
            function(t, o) { return Math.max(t.outerWidth(), t.outerHeight()) / o.outerWidth() * 2.5 },
        e.prototype.getRelX = function(t, o) {
            var i = t.offset();
            return r.isTouch()
                ? (o = o.originalEvent, 1 === o.touches.length ? o.touches[0].pageX - i.left : !1)
                : o.pageX - i.left;
        }, e.prototype.getRelY = function(t, o) {
            var i = t.offset();
            return r.isTouch()
                ? (o = o.originalEvent, 1 === o.touches.length ? o.touches[0].pageY - i.top : !1)
                : o.pageY - i.top;
        }, e.prototype.getRipplesColor = function(t) {
            var i = t.data("ripple-color") ? t.data("ripple-color") : o.getComputedStyle(t[0]).color;
            return i;
        }, e.prototype.hasTransitionSupport = function() {
            var t = i.body || i.documentElement,
                o = t.style,
                e = o.transition !== n ||
                    o.WebkitTransition !== n ||
                    o.MozTransition !== n ||
                    o.MsTransition !== n ||
                    o.OTransition !== n;
            return e;
        }, e.prototype.isTouch =
            function() {
                return /Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent);
            }, e.prototype.rippleEnd =
            function(t) { t.data("animating", "off"), "off" === t.data("mousedown") && r.rippleOut(t) },
        e.prototype.rippleOut = function(t) {
            t.off(), r.hasTransitionSupport()
                ? t.addClass("ripple-out")
                : t.animate({ opacity: 0 }, 100, function() { t.trigger("transitionend") }), t.on(
                "transitionend webkitTransitionEnd oTransitionEnd MSTransitionEnd",
                function() { t.remove() });
        }, e.prototype.rippleOn = function(t, o) {
            var i = r.getNewSize(t, o);
            r.hasTransitionSupport()
                ? o.css({
                    "-ms-transform": "scale(" + i + ")",
                    "-moz-transform": "scale(" + i + ")",
                    "-webkit-transform": "scale(" + i + ")",
                    transform: "scale(" + i + ")"
                }).addClass("ripple-on").data("animating", "on").data("mousedown", "on")
                : o.animate({
                        width: 2 * Math.max(t.outerWidth(), t.outerHeight()),
                        height: 2 * Math.max(t.outerWidth(), t.outerHeight()),
                        "margin-left": -1 * Math.max(t.outerWidth(), t.outerHeight()),
                        "margin-top": -1 * Math.max(t.outerWidth(), t.outerHeight()),
                        opacity: .2
                    },
                    500,
                    function() { o.trigger("transitionend") });
        }, t.fn.ripples = function(o) {
            return this.each(function() { t.data(this, "plugin_" + a) || t.data(this, "plugin_" + a, new e(this, o)) });
        };
}(jQuery, window, document);