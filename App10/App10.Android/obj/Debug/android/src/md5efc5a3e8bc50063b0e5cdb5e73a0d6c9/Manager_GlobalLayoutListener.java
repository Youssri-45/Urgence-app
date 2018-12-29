package md5efc5a3e8bc50063b0e5cdb5e73a0d6c9;


public class Manager_GlobalLayoutListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.view.ViewTreeObserver.OnGlobalLayoutListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onGlobalLayout:()V:GetOnGlobalLayoutHandler:Android.Views.ViewTreeObserver/IOnGlobalLayoutListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Plugin.Toasts.Manager+GlobalLayoutListener, Plugin.Toasts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Manager_GlobalLayoutListener.class, __md_methods);
	}


	public Manager_GlobalLayoutListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Manager_GlobalLayoutListener.class)
			mono.android.TypeManager.Activate ("Plugin.Toasts.Manager+GlobalLayoutListener, Plugin.Toasts, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onGlobalLayout ()
	{
		n_onGlobalLayout ();
	}

	private native void n_onGlobalLayout ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
